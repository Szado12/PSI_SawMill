namespace OrderMicroservice.ModelViews
{
    public class ModifyOrderView
    {
        public int? OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int ClientId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? AcceptanceDate { get; set; }
        public int OrderStateId { get; set; }
        public List<ModifyOrderDetailView> OrderDetails { get; set; }
    }
}
