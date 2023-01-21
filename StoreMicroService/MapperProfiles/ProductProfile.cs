using AutoMapper;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.Address;
using StoreMicroService.ViewModels.Product;

namespace StoreMicroService.MapperProfiles
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<Product, GetProductViewModel>()
        .ForMember(d => d.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
        .ForMember(d => d.WoodTypeName, opt => opt.MapFrom(src => src.WoodType.Name));
    }
  }
}
