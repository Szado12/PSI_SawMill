namespace StoreMicroService.ViewModels.Product
{
    public class GetProductViewModel
    {
        public int ProductId { get; set; }
        public double Price { get; set; }
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; }
        public int WoodTypeId { get; set; }
        public string WoodTypeName { get; set; }
    }
}
