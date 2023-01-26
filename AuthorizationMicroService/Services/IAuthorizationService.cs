using CSharpFunctionalExtensions;
using AuthorizationMicroService.ViewModels;

namespace AuthorizationMicroService.Services
{
  public interface IAuthorizationService
  {
    Result<UserData> SignIn(string login, string password);
  }
}
