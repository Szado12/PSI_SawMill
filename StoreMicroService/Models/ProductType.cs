using System;
using System.Collections.Generic;

namespace StoreMicroService.Models;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string Name { get; set; } = null!;
    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
