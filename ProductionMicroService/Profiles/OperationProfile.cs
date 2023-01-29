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
      CreateMap<Operation, GetDetailsOperationViewModel>()
        .ForMember(dest => dest.SourceProductName, opt => opt.MapFrom(src => src.SourceProductType.Name))
        .ForMember(dest => dest.OutputProductName, opt => opt.MapFrom(src => src.OutputProductType.Name));
    }
  }
}
