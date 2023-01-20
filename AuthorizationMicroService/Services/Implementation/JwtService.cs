using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AuthorizationMicroService.Models;
using AuthorizationMicroService.ViewModels;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AuthorizationMicroService.Services.Implementation
{
  public class JwtService : BaseService, IJwtService
  {
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public Result<Tuple<string, string>> RefreshExpiredToken(string expiredToken, string refreshToken)
    {
      ClaimsPrincipal claims = GetPrincipalFromExpiredToken(expiredToken);
      int userId = int.Parse(claims.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value);
      var loginData = AuthorizationContext.Employees.Include((Employee x) => x.LoginData).First((Employee x) => x.EmployeeId == userId);
      if (loginData == null)
      {
        return Result.Failure<Tuple<string, string>>("User not found");
      }
      if (loginData.LoginData.First().RefreshToken != refreshToken)
      {
        return Result.Failure<Tuple<string, string>>("Incorrect refresh token");
      }
      if (loginData.LoginData.First().RefreshTokenExpireDate < DateTime.Now)
      {
        return Result.Failure<Tuple<string, string>>("Refresh token expired");
      }

      UserData userData = new UserData
      {
        LoginId = loginData.LoginData.First().LoginId,
        Name = loginData.FirstName,
        Surname = loginData.LastName,
        RoleId = 1,
        UserId = loginData.EmployeeId
      };
      return Result.Success(new Tuple<string, string>(GenerateToken(userData), refreshToken));
    }

    private string GenerateToken(UserData userData)
    {
      double tokenValidityInMinutes = double.Parse(_configuration["JWT:TokenValidityInMinutes"]);
      SecurityTokenDescriptor securityTokenDescriptor = new SecurityTokenDescriptor();
      securityTokenDescriptor.Subject = new ClaimsIdentity(new Claim[4]
      {
      new Claim(ClaimTypes.NameIdentifier, userData.UserId.ToString()),
      new Claim(ClaimTypes.Name, userData.Name),
      new Claim(ClaimTypes.Surname, userData.Surname),
      new Claim(ClaimTypes.Role, userData.RoleId.ToString())
      });
      securityTokenDescriptor.Expires = DateTime.UtcNow.AddMinutes(tokenValidityInMinutes);
      securityTokenDescriptor.SigningCredentials = new SigningCredentials(
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])), SecurityAlgorithms.HmacSha512);
      SecurityTokenDescriptor tokenDescriptor = securityTokenDescriptor;
      SecurityToken token = new JwtSecurityTokenHandler().CreateToken(tokenDescriptor);
      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public string GenerateRefreshToken(int loginDataId)
    {
      byte[] randomNumber = new byte[64];
      using RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
      randomNumberGenerator.GetBytes(randomNumber);
      string refreshToken = Convert.ToBase64String(randomNumber);
      SaveRefreshToken(loginDataId, refreshToken);
      return refreshToken;
    }

    private void SaveRefreshToken(int loginDataId, string token)
    {
      LoginData loginData = AuthorizationContext.LoginData.First((LoginData x) => x.LoginId == loginDataId);
      loginData.RefreshToken = token;
      loginData.RefreshTokenExpireDate = DateTime.Now.AddDays(double.Parse(_configuration["JWT:RefreshTokenValidityInDays"]));
      AuthorizationContext.SaveChanges();
    }

    private ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
    {
      TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
      {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"])),
        ValidateLifetime = false
      };
      JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
      SecurityToken securityToken;
      ClaimsPrincipal principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);
      if (!(securityToken is JwtSecurityToken))
      {
        throw new SecurityTokenException("Invalid token");
      }
      return principal;
    }

    Tuple<string, string> IJwtService.LogIn(UserData userData)
    {
      if (userData == null)
      {
        return null;
      }
      string token = GenerateToken(userData);
      string refreshToken = GenerateRefreshToken(userData.LoginId);
      return new Tuple<string, string>(token, refreshToken);
    }
  }

}
