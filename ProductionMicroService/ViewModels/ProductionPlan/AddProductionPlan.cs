namespace ProductionMicroService.ViewModels.ProductionPlan
{
  public class AddProductionPlan
  {
    public double MaterialAmount { get; set; }

    public DateTime StartDate { get; set; }

    public int MachineId { get; set; }

    public int OperationId { get; set; }

    public int EmployeeId { get; set; }

    public int ProductId { get; set; }
  }
}
