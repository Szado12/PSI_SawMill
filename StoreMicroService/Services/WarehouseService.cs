using CSharpFunctionalExtensions;
using StoreMicroService.Services.Interfaces;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.Services
{
  public class WarehouseService : BaseService,IWarehouseService
  {
    public Result<string> AddWarehouse(AddWarehouseViewModel addWarehouse)
    {
      throw new NotImplementedException();
    }

    public Result<string> UpdateWarehouse(AddWarehouseViewModel addWarehouse)
    {
      throw new NotImplementedException();
    }

    public Result<List<WarehouseViewModel>> GetWarehouses()
    {
      throw new NotImplementedException();
    }

    public Result<WarehouseDetailsViewModel> GetWarehouseDetails(int warehouseId)
    {
      throw new NotImplementedException();
    }
  }
}
