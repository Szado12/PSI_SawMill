using System;
using System.Collections.Generic;

namespace ProductionMicroService.Models;

public partial class LoginDatum
{
    public int LoginId { get; set; }

    public string Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpireDate { get; set; }

    public int EmployeeId { get; set; }

    public virtual Employee Employee { get; set; } = null!;
}
