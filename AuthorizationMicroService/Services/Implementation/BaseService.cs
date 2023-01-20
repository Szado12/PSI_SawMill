using AuthorizationMicroService.Models;

namespace AuthorizationMicroService.Services.Implementation
{
  public class BaseService
  {
    public readonly AuthorizationContext AuthorizationContext = new AuthorizationContext();
  }
}
