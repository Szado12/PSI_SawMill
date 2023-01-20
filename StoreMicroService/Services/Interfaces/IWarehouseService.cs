using CSharpFunctionalExtensions;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.Services.Interfaces
{
  public interface IWarehouseService
  {
    Result<string> AddWarehouse(AddWarehouseViewModel addWarehouse);
    Result<string> UpdateWarehouse(AddWarehouseViewModel addWarehouse);

    Result<List<WarehouseViewModel>> GetWarehouses();
    Result<WarehouseDetailsViewModel> GetWarehouseDetails(int warehouseId);

  }
}
