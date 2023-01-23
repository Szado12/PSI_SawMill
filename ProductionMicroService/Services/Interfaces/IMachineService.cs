namespace ProductionMicroService.Services.Interfaces
{
  public interface IMachineService
  {
    public string AddMachine();
    public string DeleteMachine();
    public string UpdateMachine();
    public List<object> GetAllMachines();
    public List<object> GetAllMachinesByState();
    public List<object> GetAllMachinesByOperation();
  }
}
