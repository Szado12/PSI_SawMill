using AutoMapper;
using ProductionMicroService.Models;
using ProductionMicroService.ViewModels.Operation;
using ProductionMicroService.ViewModels.ProductionPlan;

namespace ProductionMicroService.Profiles
{
  public class ProductionPlanProfile : Profile
  {
    public ProductionPlanProfile()
    {
      CreateMap<AddProductionPlan, ProductionDetail>();
      CreateMap<UpdateOperationViewModel, ProductionDetail>();
      CreateMap<ProductionDetail, GetProductionPlanViewModel>();
    }
  }
}
