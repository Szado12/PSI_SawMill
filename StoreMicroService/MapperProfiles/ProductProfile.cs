using AutoMapper;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.Address;
using StoreMicroService.ViewModels.Product;
using StoreMicroService.ViewModels.ProductType;
using StoreMicroService.ViewModels.WoodType;

namespace StoreMicroService.MapperProfiles
{
  public class ProductProfile : Profile
  {
    public ProductProfile()
    {
      CreateMap<Product, GetProductViewModel>()
        .ForMember(d => d.ProductTypeName, opt => opt.MapFrom(src => src.ProductType.Name))
        .ForMember(d => d.WoodTypeName, opt => opt.MapFrom(src => src.WoodType.Name));
      CreateMap<WoodType, WoodTypeModel>();
      CreateMap<ProductType, ProductTypeModel>();
    }
  }
}
