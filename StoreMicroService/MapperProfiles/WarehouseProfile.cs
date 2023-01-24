using AutoMapper;
using StoreMicroService.Models;
using StoreMicroService.ViewModels.Address;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.MapperProfiles
{
  public class WarehouseProfile : Profile
  {
    public WarehouseProfile()
    {
      CreateMap<Address, AddressViewModel>();
      CreateMap<AddressViewModel, Address>();
      CreateMap<AddWarehouseViewModel, Warehouse>();
      CreateMap<UpdateWarehouseViewModel, Warehouse>();
      CreateMap<WarehousesToProduct, StoredProductsViewModel>()
        .ForMember(d => d.ProductTypeName, opt => opt.MapFrom(src => src.Product.ProductType.Name))
        .ForMember(d => d.WoodTypeName, opt => opt.MapFrom(src => src.Product.WoodType.Name))
        .ForMember(d => d.Price, opt => opt.MapFrom(src => src.Product.Price));

      CreateMap<Warehouse, WarehouseViewModel>()
        .ForMember(s => s.CurrentCapacity,
          opt => opt.MapFrom(src => src.WarehousesToProducts.Sum(x => x.Amount)));
      CreateMap<Warehouse, WarehouseDetailsViewModel>();
    }
  }
}
