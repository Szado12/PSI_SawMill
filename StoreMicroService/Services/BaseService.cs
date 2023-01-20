using StoreMicroService.Models;

namespace StoreMicroService.Services
{
  public class BaseService
  {
    public readonly StoreContext StoreContext = new StoreContext();
  }
}
