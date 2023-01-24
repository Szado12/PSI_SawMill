using System;
using System.Collections.Generic;

namespace EmployeeMicroservice.Models;

public partial class LoginData
{
    public int LoginId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpireDate { get; set; }

    public int EmployeeId { get; set; }

    public byte[] Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
