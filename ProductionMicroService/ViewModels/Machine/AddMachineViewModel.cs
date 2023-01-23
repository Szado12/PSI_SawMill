using ProductionMicroService.Models;

namespace ProductionMicroService.ViewModels.Machine
{
  public class AddMachineViewModel
  {
    public string Name { get; set; } = null!;
    public List<int> AvailableOperationIds { get; set; }
  }
}
