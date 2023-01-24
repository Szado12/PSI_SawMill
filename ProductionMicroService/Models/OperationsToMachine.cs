using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class OperationsToMachine
{
    public int Id { get; set; }

    public int MachineId { get; set; }

    public int OperationId { get; set; }

    public virtual Machine Machine { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;
}
