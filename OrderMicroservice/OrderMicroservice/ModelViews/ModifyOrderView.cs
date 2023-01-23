namespace OrderMicroservice.ModelViews
{
    public class ModifyOrderView
    {
        public int? OrderId { get; set; }
        public int ClientId { get; set; }
        public List<ModifyOrderDetailView> OrderDetails { get; set; }
    }
}
