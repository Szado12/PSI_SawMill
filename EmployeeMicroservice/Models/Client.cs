using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class Client
{
    public int ClientId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string Nip { get; set; } = null!;

    public int AddressId { get; set; }

    public byte[] FirstName { get; set; } = null!;

    public byte[] LastName { get; set; } = null!;

    public bool IsArchived { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
