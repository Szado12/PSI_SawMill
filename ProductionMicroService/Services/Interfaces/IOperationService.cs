using CSharpFunctionalExtensions;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Services.Interfaces
{
  public interface IOperationService
  {
    Result<int> AddOperation(AddOperationViewModel addOperationViewModel);
    Result<int> UpdateOperation(UpdateOperationViewModel operationViewModel);
    Result<int> DeleteOperation(int operationId);
    Result<GetDetailsOperationViewModel> GetOperationById(int operationId);
    Result<List<GetOperationViewModel>> GetAllOperations();
    Result<List<GetOperationViewModel>> GetAllOperationsByMachine(int machineId);
  }
}
