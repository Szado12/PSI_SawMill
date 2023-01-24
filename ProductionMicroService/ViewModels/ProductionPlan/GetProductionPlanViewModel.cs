using ProductionMicroService.ViewModels.Machine;
using ProductionMicroService.ViewModels.Operation;

namespace ProductionMicroService.ViewModels.ProductionPlan
{
  public class GetProductionPlanViewModel
  {
    public int ProductionDetailId { get; set; }

    public double MaterialAmount { get; set; }

    public DateTime StartDate { get; set; }
    
    public int MachineId { get; set; }

    public int OperationId { get; set; }

    public int EmployeeId { get; set; }

    public int ProductId { get; set; }

    public int StatusId { get; set; }
    public string Status { get; set; }

    public virtual GetMachineViewModel Machine { get; set; } = null!;

    public virtual GetOperationViewModel Operation { get; set; } = null!;
  }
}
