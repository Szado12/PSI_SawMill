namespace OrderMicroservice.ModelViews
{
    public class ClientDetails
    {
        public int? ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressView Address { get; set; }
        public string CompanyName { get; set; }
        public string NIP { get; set; }
        public bool? IsArchived { get; set; } = false;
    }
}
