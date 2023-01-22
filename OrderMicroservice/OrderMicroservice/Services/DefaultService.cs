using AutoMapper;
using OrderMicroservice.MapperProfiles;
using OrderMicroservice.Models;

namespace OrderMicroservice.Services
{
    public class DefaultService
    {
        protected ClientOrderContext ClientOrderContext;
        protected IMapper Mapper;
        public DefaultService(ClientOrderContext clientOrderContext)
        {
            ClientOrderContext = clientOrderContext;
            Mapper = new MapperConfiguration(
              cfg => cfg.AddProfiles(new List<Profile>
                {
                    new ClientProfile()
                })).CreateMapper();
        }
    }
}
