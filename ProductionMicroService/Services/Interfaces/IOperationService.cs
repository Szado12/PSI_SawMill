namespace ProductionMicroService.Services.Interfaces
{
  public interface IOperationService
  {
    public string AddOperation();
    public string UpdateOperation();
    public string ArchiveOperation();
    public List<object> GetAllOperations();
    public List<object> GetAllOperationsByMachine();
  }
}
