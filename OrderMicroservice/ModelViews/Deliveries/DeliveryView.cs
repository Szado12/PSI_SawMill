namespace OrderMicroservice.ModelViews.Deliveries
{
    public class DeliveryView
    {
        public int DeliveryId { get; set; }
        public int OrderId { get; set; }
        public DateTime OrderCreationDate { get; set; }
        public DateTime? SendDate { get; set; }
        public DelivererView Deliverer { get; set; }
        public DeliveryClientView Client { get; set; }
        public AddressView DeliveryAddress { get; set; }
        public string State { get; set; }
    }
}
