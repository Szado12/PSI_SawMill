using AutoMapper;
using EmployeeMicroservice.Models;
using EmployeeMicroservice.Services;
using EmployeeMicroservice.Services.Interfaces;
using EmployeeMicroservice.ViewModels;

namespace EmployeeMicroservice.Mapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile() 
        {
            EncryptionService encryptionService = new EncryptionService();

            CreateMap<Employee, EmployeeView>()
                .ForMember(x => x.EmployeeType, opt => opt.MapFrom(src => src.EmployeeType.Name))
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.FirstName)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.DecryptData(src.LastName)));

            CreateMap<AddEmployeeView, Employee>()
                .ForMember(x => x.EmployeeType, opt => opt.Ignore())
                .ForMember(x => x.EmployeeId, opt => opt.Ignore())
                .ForMember(x => x.LoginData, opt => opt.Ignore())
                .ForMember(x => x.FirstName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.FirstName)))
                .ForMember(x => x.LastName, opt => opt.MapFrom(src => encryptionService.EncryptData(src.LastName)));

            CreateMap<AddEmployeeView, LoginData>()
                .ForMember(x => x.Password, opt => opt.MapFrom(src => encryptionService.HashData(src.Password)))
                .ForMember(x => x.Login, opt => opt.MapFrom(src => encryptionService.HashData(src.Login)));

            CreateMap<EmployeeType, EmployeeTypeView>();
        }
    }
}
