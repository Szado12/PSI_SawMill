using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class Machine
{
    public int MachineId { get; set; }

    public string Name { get; set; } = null!;

    public bool IsBroken { get; set; }

    public bool IsArchived { get; set; }

    public virtual ICollection<OperationsToMachine> OperationsToMachines { get; } = new List<OperationsToMachine>();

    public virtual ICollection<ProductionDetail> ProductionDetails { get; } = new List<ProductionDetail>();
}
