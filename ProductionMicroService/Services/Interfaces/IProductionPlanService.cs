namespace ProductionMicroService.Services.Interfaces
{
  public interface IProductionPlanService
  {
    //ADd
    //Modify/Update
    //Delete -> archive
    List<object> GetProductionPlan();

    List<object> GetProductionPlanByEmployee();
    List<object> GetProductionPlanByMachine();

    List<object> CheckAvailability();

  }
}
