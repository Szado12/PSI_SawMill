using StoreMicroService.ViewModels.Address;

namespace StoreMicroService.ViewModels.Warehouse
{
  public class UpdateWarehouseViewModel
  {
    public int WarehouseId { get; set; }
    public double Capacity { get; set; }
    public string Name { get; set; }
    public virtual AddressViewModel Address { get; set; }
    public List<StoredProductsViewModel> StoredProducts { get; set; }
  }
}
