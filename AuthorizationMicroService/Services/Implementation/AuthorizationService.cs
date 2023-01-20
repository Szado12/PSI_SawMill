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
    string login2 = login;
    byte[] hashedPassword = _encryptionService.HashString(password);
    LoginData loginData = AuthorizationContext.LoginData.Include((LoginData x) => x.Employee).Single((LoginData x) => x.Login == login2 && x.Password == hashedPassword);
    if (loginData == null)
    {
      return Result.Failure<UserData>("Incorrect login data");
    }
    UserData userData = new UserData
    {
      LoginId = loginData.LoginId,
      UserId = loginData.Employee.EmployeeId,
      Name = loginData.Employee.FirstName,
      Surname = loginData.Employee.LastName
    };
    return Result.Success(userData);
  }

  public void AddSyf()
  {
    int i = 0;
    while (true)
    {
      if (i < 10)
      {
        string stringName = "test";
        Employee employee = new Employee
        {
          FirstName = "test" + i,
          LastName = "surname" + i,
          IsArchived = false,
          IsFaired = (i % 2 == 0),
          WorkStartDate = DateTime.Now
        };
        AuthorizationContext.Employees.Add(employee);
        AuthorizationContext.SaveChanges();
        int id = AuthorizationContext.Employees.First((Employee x) => x.FirstName == "test" + i.ToString() && x.LastName == "surname" + i.ToString()).EmployeeId;
        LoginData login = new LoginData
        {
          EmployeeId = id,
          Password = _encryptionService.HashString("test" + i),
          Login = "test" + i
        };
        AuthorizationContext.LoginData.Add(login);
        AuthorizationContext.SaveChanges();
        i++;
        continue;
      }
      break;
    }
  }
}