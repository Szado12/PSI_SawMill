using System;
using System.Collections.Generic;

namespace StoreMicroService.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public virtual ICollection<Warehouse> Warehouses { get; } = new List<Warehouse>();
}
