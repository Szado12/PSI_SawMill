using CSharpFunctionalExtensions;
using AuthorizationMicroService.ViewModels;

namespace AuthorizationMicroService.Services
{
  public interface IJwtService
  {
    Tuple<string, string> LogIn(UserData userData);
    Result<Tuple<string, string>> RefreshExpiredToken(string expiredToken, string refreshToken);
  }
}
