using ProductionMicroService.Models;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.ViewModels.Machine
{
  public class GetMachineViewModel
  {
    public int MachineId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsBroken { get; set; }

    public bool IsArchived { get; set; }

    public List<GetOperationViewModel> AllowedOperations { get; set; }
  }
}
