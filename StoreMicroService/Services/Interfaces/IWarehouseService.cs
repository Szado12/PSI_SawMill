using CSharpFunctionalExtensions;
using StoreMicroService.ViewModels.Warehouse;

namespace StoreMicroService.Services.Interfaces
{
  public interface IWarehouseService
  {
    Result<int> AddWarehouse(AddWarehouseViewModel addWarehouse);
    Result<int> UpdateWarehouse(UpdateWarehouseViewModel addWarehouse);

    Result<List<WarehouseViewModel>> GetWarehouses();
    Result<WarehouseDetailsViewModel> GetWarehouseDetails(int warehouseId);

  }
}
