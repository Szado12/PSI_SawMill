using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class ProductionDetail
{
    public int ProductionDetailId { get; set; }

    public double MaterialAmount { get; set; }

    public DateTime StartDate { get; set; }

    public bool IsArchived { get; set; }

    public int MachineId { get; set; }

    public int OperationId { get; set; }

    public int EmployeeId { get; set; }

    public int ProductId { get; set; }

    public int Status { get; set; }

    public virtual Machine Machine { get; set; } = null!;

    public virtual Operation Operation { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
