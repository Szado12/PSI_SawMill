using AutoMapper;
using ProductionMicroService.Models;
using ProductionMicroService.Profiles;

namespace ProductionMicroService.Services
{
  public class DefaultService
  {
    protected ProductionContext ProductionContext;
    protected IMapper Mapper;
    public DefaultService(ProductionContext productionContext)
    {
      ProductionContext = productionContext;
      Mapper = new MapperConfiguration(
        cfg => cfg.AddProfiles(new List<Profile>
        {
          new OperationProfile(),
          new MachineProfile(),
          new ProductionPlanProfile()
        })).CreateMapper();
    }
  }
}
