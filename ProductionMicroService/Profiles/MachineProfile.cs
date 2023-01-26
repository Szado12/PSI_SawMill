using AutoMapper;
using ProductionMicroService.Models;
using ProductionMicroService.ViewModels.Machine;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Profiles
{
  public class MachineProfile : Profile
  {
    public MachineProfile()
    {
      CreateMap<Machine, GetMachineViewModel>().ForMember(dest => dest.AllowedOperations,
        opt => opt.MapFrom(src => src.OperationsToMachines.Select(z => z.Operation)));
      CreateMap<AddMachineViewModel, Machine>();
      CreateMap<UpdateMachineViewModel, Machine>();
    }
  }
}
