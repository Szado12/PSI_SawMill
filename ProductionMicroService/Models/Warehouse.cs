using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public double Capacity { get; set; }

    public int AddressId { get; set; }

    public string Name { get; set; } = null!;

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<WarehousesToProduct> WarehousesToProducts { get; } = new List<WarehousesToProduct>();
}
