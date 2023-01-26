using AutoMapper;
using ProductionMicroService.Models;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Profiles
{
  public class OperationProfile : Profile
  {
    public OperationProfile()
    {
      CreateMap<Operation, GetOperationViewModel>();
      CreateMap<AddOperationViewModel, Operation>();
      CreateMap<UpdateOperationViewModel, Operation>();
    }
  }
}
