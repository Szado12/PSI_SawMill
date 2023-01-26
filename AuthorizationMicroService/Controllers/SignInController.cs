using AuthorizationMicroService.Services;
using AuthorizationMicroService.ViewModels;
using Microsoft.AspNetCore.Mvc;
using CSharpFunctionalExtensions;

namespace AuthorizationMicroService.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class SignInController : ControllerBase
  {
    private IAuthorizationService _authorizationService;

    private IJwtService _jwtService;

    public SignInController(IAuthorizationService authorizationService, IJwtService jwtService)
    {
      _authorizationService = authorizationService;
      _jwtService = jwtService;
    }
    
    [HttpPost]
    [Route("refreshToken")]
    public IActionResult RefreshExpiredToken(string expiredToken, string refreshToken)
    {
      Result<Tuple<string, string>> result = _jwtService.RefreshExpiredToken(expiredToken, refreshToken);
      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }
      return Ok(result.Value);
    }

    [HttpPost]
    [Route("singIn")]
    public IActionResult SignIn(string loginName, string hashedPassword)
    {
      Result<UserData> result = _authorizationService.SignIn(loginName, hashedPassword);
      if (result.IsFailure)
      {
        return BadRequest(result.Error);
      }
      return Ok(_jwtService.LogIn(result.Value));
    }
  }
}
