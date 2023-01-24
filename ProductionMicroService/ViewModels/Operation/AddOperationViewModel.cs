namespace ProductionMicroService.ViewModels.Operation
{
  public class AddOperationViewModel
  {
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int SourceProductTypeId { get; set; }
    public int OutputProductTypeId { get; set; }
    public double SourceOutputRatio { get; set; }
    public double Duration { get; set; }
  }
}
