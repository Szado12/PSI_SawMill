using StoreMicroService.ViewModels.Address;

namespace StoreMicroService.ViewModels.Warehouse
{
  public class AddWarehouseViewModel
  {
    public double Capacity { get; set; }
    public string Name { get; set; }
    public virtual AddressViewModel Address { get; set; }
  }
}
