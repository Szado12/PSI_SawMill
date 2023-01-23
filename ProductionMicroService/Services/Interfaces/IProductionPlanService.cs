using CSharpFunctionalExtensions;
using ProductionMicroService.ViewModels.ProductionPlan;

namespace ProductionMicroService.Services.Interfaces
{
  public interface IProductionPlanService
  {
    Result<string> AddProductionPlan(AddProductionPlan addProductionPlan);
    Result<string> ModifyProductionPlan(UpdateProductionPlanViewModel productionPlanToBeUpdated);
    Result<string> DeleteProductionPlan(int productionPlanId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlan();
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByEmployee(int employeeId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByMachine(int machineId);
    Result<List<bool>> CheckAvailability();
  }
}
