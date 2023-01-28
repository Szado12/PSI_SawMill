using CSharpFunctionalExtensions;
using AuthorizationMicroService.ViewModels;

namespace AuthorizationMicroService.Services
{
  public interface IJwtService
  {
    UserDataWithTokens LogIn(UserData userData);
    Result<Tuple<string, string>> RefreshExpiredToken(string expiredToken, string refreshToken);
  }
}
