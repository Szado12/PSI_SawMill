using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class ProductType
{
    public int ProductTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Operation> OperationOutputProductTypes { get; } = new List<Operation>();

    public virtual ICollection<Operation> OperationSourceProductTypes { get; } = new List<Operation>();

    public virtual ICollection<Product> Products { get; } = new List<Product>();
}
