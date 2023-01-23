namespace OrderMicroservice.ModelViews.Orders
{
    public class OrderView
    {
        public int? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public ClientDetails ClientDetails { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public string OrderState { get; set; }
        public List<OrderDetailsView> OrderDetails { get; set; }
        public decimal OrderPrice { get; set; }
    }
}
