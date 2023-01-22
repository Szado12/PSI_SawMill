using AutoMapper;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services;

namespace OrderMicroservice.MapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDetail, OrderDetailsView>(MemberList.None)
                .ForMember(x => x.ProductType, opt => opt.MapFrom(src => src.Product.ProductType.Name))
                .ForMember(x => x.WoodType, opt => opt.MapFrom(src => src.Product.WoodType.Name))
                .ForMember(x => x.FullPrice, opt => opt.MapFrom(src => Int32.Parse(src.Amount) * src.Product.Price))
                .ForAllMembers(x => x.Ignore());

            CreateMap<Order, OrderView>(MemberList.None)
                .ForMember(x => x.OrderState , opt => opt.MapFrom(src => src.OrderState.Name))
                .ForMember(x => x.ClientDetails, opt => opt.MapFrom(src => src.Client))
                .ForMember(x => x.OrderDetails, opt => opt.MapFrom(src => (OrderDetailsView)src.OrderDetails))
                .ForMember(x => x.OrderPrice, opt => opt.MapFrom(src => src.OrderDetails.Sum(d => Int32.Parse(d.Amount) * d.Product.Price)))
                .ForAllMembers(x => x.Ignore());
        }
    }
}
