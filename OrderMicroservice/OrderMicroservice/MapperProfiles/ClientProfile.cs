using AutoMapper;
using OrderMicroservice.Models;
using OrderMicroservice.ModelViews;
using OrderMicroservice.Services;

namespace OrderMicroservice.MapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile()
        {
            EncryptionService encryptionService = new EncryptionService();

            CreateMap<Client, ClientDetails>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.FirstName)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.LastName)));

            CreateMap<ClientDetails, Client>()
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.FirstName)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.LastName)));

            CreateMap<Address, AddressView>();
            CreateMap<AddressView, Address>();
        }
    }
}
