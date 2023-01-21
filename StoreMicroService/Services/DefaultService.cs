using AutoMapper;
using StoreMicroService.MapperProfiles;
using StoreMicroService.Models;

namespace StoreMicroService.Services
{
  public class DefaultService
  {
    protected StoreContext StoreContext;
    protected IMapper Mapper;
    public DefaultService(StoreContext storeContext)
    {
      StoreContext = storeContext;
      Mapper = new MapperConfiguration(
        cfg => cfg.AddProfiles(new List<Profile> 
          {
            new WarehouseProfile(),
            new ProductProfile()

          })).CreateMapper();
    }
  }
}
