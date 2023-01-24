namespace ProductionMicroService.ViewModels.Machine
{
  public class UpdateMachineViewModel
  {
    public int MachineId { get; set; }
    public string Name { get; set; }
    public bool IsBroken { get; set; }
    public List<int> AvailableOperationIds { get; set; }
  }
}
