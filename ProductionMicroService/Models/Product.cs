using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public double Price { get; set; }

    public int ProductTypeId { get; set; }

    public int WoodTypeId { get; set; }

    public virtual ProductType ProductType { get; set; } = null!;

    public virtual ICollection<ProductionDetail> ProductionDetails { get; } = new List<ProductionDetail>();

    public virtual ICollection<WarehousesToProduct> WarehousesToProducts { get; } = new List<WarehousesToProduct>();

    public virtual WoodType WoodType { get; set; } = null!;
}
