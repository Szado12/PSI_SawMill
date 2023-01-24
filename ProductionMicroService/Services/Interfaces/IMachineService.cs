using CSharpFunctionalExtensions;
using ProductionMicroService.ViewModels.Machine;

namespace ProductionMicroService.Services.Interfaces
{
  public interface IMachineService
  {
    public Result<int> AddMachine(AddMachineViewModel addMachine);
    public Result<int> DeleteMachine(int machineId);
    public Result<int> UpdateMachine(UpdateMachineViewModel updateMachine);
    public Result<GetMachineViewModel> GetMachineById(int machineId);
    public Result<List<GetMachineViewModel>> GetAllMachines();
    public Result<List<GetMachineViewModel>> GetAllMachinesByState();
    public Result<List<GetMachineViewModel>> GetAllMachinesByOperation(int operationId);
  }
}
