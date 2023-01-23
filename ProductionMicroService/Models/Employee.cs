using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateTime WorkStartDate { get; set; }

    public DateTime? WorkEndDate { get; set; }

    public bool IsArchived { get; set; }

    public bool IsFaired { get; set; }

    public virtual ICollection<LoginDatum> LoginData { get; } = new List<LoginDatum>();
}
