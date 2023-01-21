using StoreMicroService.ViewModels.Address;

namespace StoreMicroService.ViewModels.Warehouse
{
  public class WarehouseDetailsViewModel
  {
    public int WarehouseId { get; set; }

    public double Capacity { get; set; }
    public double CurrentCapacity { get; set; }
    public string Name { get; set; }
    public AddressViewModel Address { get; set; }

    public List<StoredProductsViewModel> WarehousesToProducts { get; set; }
  }
}
