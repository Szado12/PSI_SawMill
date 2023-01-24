using System;
using System.Collections.Generic;

namespace OrderMicroservice.Models;

public partial class LoginDatum
{
    public int LoginId { get; set; }

    public string? RefreshToken { get; set; }

    public DateTime? RefreshTokenExpireDate { get; set; }

    public int EmployeeId { get; set; }

    public byte[] Login { get; set; } = null!;

    public byte[] Password { get; set; } = null!;

    public virtual Employee Employee { get; set; } = null!;
}
