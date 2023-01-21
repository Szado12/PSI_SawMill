using AutoMapper;
using EmployeeMicroservice.Mapper;
using EmployeeMicroservice.Models;

namespace EmployeeMicroservice.Services
{
    public class DefaultService
    {
        public readonly EmployeeContext EmployeeContext = new EmployeeContext();
        private static readonly MapperConfiguration mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MapperProfile()));
        protected IMapper Mapper = mapperConfig.CreateMapper();
    }
}
