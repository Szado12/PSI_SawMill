using AutoMapper;
using StoreMicroService.MapperProfiles;
using StoreMicroService.Models;

namespace StoreMicroService.Services
{
  public class DeafultService
  {
    public readonly StoreContext StoreContext = new StoreContext();
    private static readonly MapperConfiguration MapperConfig = new MapperConfiguration(
      cfg => cfg.AddProfiles(new List<Profile>{new WarehouseProfile(), new ProductProfile()}));
    protected IMapper Mapper = MapperConfig.CreateMapper();
  }
}
