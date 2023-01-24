using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? ClientId { get; set; }

    public int? OrderStateId { get; set; }

    public DateTime? CreationDate { get; set; }

    public DateTime? AcceptanceDate { get; set; }

    public int? DeliveryId { get; set; }

    public virtual Client? Client { get; set; }

    public virtual Delivery? Delivery { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; } = new List<OrderDetail>();

    public virtual OrderState? OrderState { get; set; }
}
