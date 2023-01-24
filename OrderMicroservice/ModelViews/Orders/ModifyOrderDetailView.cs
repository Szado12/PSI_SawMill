namespace OrderMicroservice.ModelViews.Orders
{
    public class ModifyOrderDetailView
    {
        public int? OrderDetailId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
    }
}
