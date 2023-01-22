using System;
using System.Collections.Generic;

namespace OrderMicroservice.Models;

public partial class WarehousesToProduct
{
    public int WarehouseId { get; set; }

    public int ProductId { get; set; }

    public double Amount { get; set; }

    public int Id { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual Warehouse Warehouse { get; set; } = null!;
}
