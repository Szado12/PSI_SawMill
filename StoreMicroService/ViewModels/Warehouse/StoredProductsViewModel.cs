namespace StoreMicroService.ViewModels.Warehouse
{
  public class StoredProductsViewModel
  {
    public int ProductId { get; set; }
    public string? ProductTypeName { get; set; }
    public string? WoodTypeName { get; set; }
    public double Amount { get; set; }
    public double Price { get; set; }
  }
}
