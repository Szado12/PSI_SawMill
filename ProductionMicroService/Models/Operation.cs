using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class Operation
{
    public int OperationId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int SourceProductTypeId { get; set; }

    public int OutputProductTypeId { get; set; }

    public double SourceOutputRatio { get; set; }

    public double Duration { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<OperationsToMachine> OperationsToMachines { get; } = new List<OperationsToMachine>();

    public virtual ProductType OutputProductType { get; set; } = null!;

    public virtual ICollection<ProductionDetail> ProductionDetails { get; } = new List<ProductionDetail>();

    public virtual ProductType SourceProductType { get; set; } = null!;
}
