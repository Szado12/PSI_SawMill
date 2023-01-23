using CSharpFunctionalExtensions;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.Services.Interfaces
{
  public interface IOperationService
  {
    Result<string> AddOperation(AddOperationViewModel addOperationViewModel);
    Result<string> UpdateOperation(UpdateOperationViewModel operationViewModel);
    Result<string> DeleteOperation(int operationId);
    Result<List<AddOperationViewModel>> GetAllOperations();
    Result<List<AddOperationViewModel>> GetAllOperationsByMachine(int machineId);
  }
}
