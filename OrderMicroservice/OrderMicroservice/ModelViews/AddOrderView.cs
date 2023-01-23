namespace OrderMicroservice.ModelViews
{
    public class AddOrderView
    {
        public int? OrderId { get; set; }
        public int? ClientId { get; set; }
        public List<ModifyOrderDetailView> OrderDetails { get; set; }
    }
}
