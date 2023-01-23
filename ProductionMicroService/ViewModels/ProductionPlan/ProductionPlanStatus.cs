namespace ProductionMicroService.ViewModels.ProductionPlan
{
  public enum ProductionPlanStatus
  {
    Created =1,
    InProgress =2,
    Completed =3,
    Failed =4
  }

  public static class ProductionPlanStatusExtensions{
    public static string EnumToString(this ProductionPlanStatus productionPlan)
    {
      switch (productionPlan)
      {
        case ProductionPlanStatus.Created:
          return "Created";
        case ProductionPlanStatus.InProgress:
          return "In Progress";
        case ProductionPlanStatus.Completed:
          return "Completed";
        case ProductionPlanStatus.Failed:
          return "Failed";
      }
      return "";
    }
  }
}
