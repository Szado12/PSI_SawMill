using System;
using System.Collections.Generic;

namespace OrderMicroservice.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public DateTime? SendDate { get; set; }

    public int DeliveryStateId { get; set; }

    public int? DelivererId { get; set; }

    public virtual Employee? Deliverer { get; set; }

    public virtual DeliveryState DeliveryState { get; set; } = null!;

    public virtual Order? Order { get; set; }
}
