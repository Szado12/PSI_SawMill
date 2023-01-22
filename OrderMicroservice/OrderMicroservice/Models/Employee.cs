﻿using System;
using System.Collections.Generic;

namespace OrderMicroservice.Models;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public bool IsBlocked { get; set; }

    public int? EmployeeTypeId { get; set; }

    public byte[] LastName { get; set; } = null!;

    public byte[] FirstName { get; set; } = null!;

    public virtual EmployeeType? EmployeeType { get; set; }

    public virtual ICollection<LoginDatum> LoginData { get; } = new List<LoginDatum>();
}
