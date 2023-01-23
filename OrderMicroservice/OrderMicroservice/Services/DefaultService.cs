using AutoMapper;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services.Interfaces;

namespace OrderMicroservice.Services
{
    public class DefaultService
    {
        protected ClientOrderContext ClientOrderContext;
        protected IMapper Mapper;
        EncryptionService encryptionService = new EncryptionService();

        public DefaultService(ClientOrderContext clientOrderContext)
        {
            ClientOrderContext = clientOrderContext;
            Mapper = new MapperConfiguration(
              cfg =>
              {
                  cfg.CreateMap<Client, ClientDetails>()
                    .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.FirstName)))
                    .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.LastName)));

                  cfg.CreateMap<ClientDetails, Client>()
                      .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.FirstName)))
                      .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.LastName)));

                  cfg.CreateMap<Address, AddressView>();

                  cfg.CreateMap<AddressView, Address>();

                  cfg.CreateMap<OrderDetail, OrderDetailsView>(MemberList.None)
                      .ForMember(x => x.ProductType, opt => opt.MapFrom(src => src.Product.ProductType.Name))
                      .ForMember(x => x.WoodType, opt => opt.MapFrom(src => src.Product.WoodType.Name))
                      .ForMember(x => x.Price, opt => opt.MapFrom(src => src.Product.Price))
                      .ForMember(x => x.FullPrice, opt => opt.MapFrom(src => Int32.Parse(src.Amount) * src.Product.Price));

                  cfg.CreateMap<Order, OrderView>(MemberList.None)
                      .ForMember(x => x.OrderState, opt => opt.MapFrom(src => src.OrderState.Name))
                      .ForMember(x => x.ClientDetails, opt => opt.MapFrom(src => src.Client))
                      .ForMember(x => x.OrderDetails, opt => opt.MapFrom(src => src.OrderDetails))
                      .ForMember(x => x.OrderPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(d => Int32.Parse(d.Amount) * d.Product.Price)));
              }).CreateMapper();
        }
    }
}
