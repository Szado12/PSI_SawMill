using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class Warehouse
{
    public int WarehouseId { get; set; }

    public double Capacity { get; set; }

    public string Adress { get; set; } = null!;

    public virtual ICollection<WarehousesToProduct> WarehousesToProducts { get; } = new List<WarehousesToProduct>();
}
