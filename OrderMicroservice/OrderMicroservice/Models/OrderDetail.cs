using System;
using System.Collections.Generic;

namespace OrderMicroservice.Models;

public partial class OrderDetail
{
    public int OrderDetailId { get; set; }

    public int ProductId { get; set; }

    public string Amount { get; set; } = null!;

    public int OrderId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
