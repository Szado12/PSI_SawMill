using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class DeliveryState
{
    public int DeliveryStateId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Delivery> Deliveries { get; } = new List<Delivery>();
}
