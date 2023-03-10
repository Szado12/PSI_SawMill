namespace ProductionMicroService.ViewModels.Operation
{
  public class UpdateOperationViewModel
  {
    public int OperationId { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    public int SourceProductTypeId { get; set; }
    public int OutputProductTypeId { get; set; }
    public double SourceOutputRatio { get; set; }
    public double Duration { get; set; }
    public bool IsArchived { get; set; }
  }
}
