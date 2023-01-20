using System;
using System.Collections.Generic;

namespace StoreMicroService.Models;

public partial class WoodType
{
    public int WoodTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
