// AuthorizationMicroService.Services.Implementation.AuthorizationService

using AuthorizationMicroService.Models;
using AuthorizationMicroService.ViewModels;
using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationMicroService.Services.Implementation;

public class AuthorizationService : BaseService, IAuthorizationService
{
  private IEncryptionService _encryptionService;

  public AuthorizationService(IEncryptionService encryptionService)
  {
    _encryptionService = encryptionService;
  }

  Result<UserData> IAuthorizationService.SignIn(string login, string password)
  {

    byte[] hashedPassword = _encryptionService.HashData(password);
    byte[] hashedLogin = _encryptionService.HashData(login);
    LoginData? loginData = AuthorizationContext.LoginData.Include(x => x.Employee).FirstOrDefault(x => x.Login == hashedLogin && x.Password == hashedPassword);
    if (loginData == null)
    {
      return Result.Failure<UserData>("Incorrect login data");
    }
    
    UserData userData = new UserData
    {
      LoginId = loginData.LoginId,
      UserId = loginData.Employee.EmployeeId,
      Name = _encryptionService.DecryptData(loginData.Employee.FirstName),
      Surname = _encryptionService.DecryptData(loginData.Employee.LastName)
    };
    return Result.Success(userData);
  }
  
}