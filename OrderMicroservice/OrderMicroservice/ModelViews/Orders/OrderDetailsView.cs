namespace OrderMicroservice.ModelViews.Orders
{
    public class OrderDetailsView
    {
        public int? OrderDetailId { get; set; }
        public string WoodType { get; set; }
        public string ProductType { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; }
        public decimal FullPrice { get; set; }

    }
}
