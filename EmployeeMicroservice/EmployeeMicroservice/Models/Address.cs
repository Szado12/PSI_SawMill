using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string City { get; set; } = null!;

    public string Street { get; set; } = null!;

    public string PostalCode { get; set; } = null!;
}
