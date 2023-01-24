using System;
using System.Collections.Generic;

namespace AuthorizationMicroService.Models;

public partial class EmployeeType
{
    public int EmployeeTypeId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
}
