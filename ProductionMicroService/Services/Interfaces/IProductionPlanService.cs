using CSharpFunctionalExtensions;
using ProductionMicroService.ViewModels.ProductionPlan;

namespace ProductionMicroService.Services.Interfaces
{
  public interface IProductionPlanService
  {
    Task<Result<int>> AddProductionPlan(AddProductionPlan addProductionPlan);
    Result<int> DeleteProductionPlan(int productionPlanId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlans();
    Result<GetProductionPlanViewModel> GetProductionPlanById(int productionPlanId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByEmployee(int employeeId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByMachine(int machineId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByOperation(int operationId);
    Result<List<GetProductionPlanViewModel>> GetProductionPlanByState(int stateId);
    Task<Result<int>> UpdateState(int productionPlanId);
    Result<int> UpdateStateError(int productionPlanId);
    Result<List<bool>> CheckAvailability();
  }
}
