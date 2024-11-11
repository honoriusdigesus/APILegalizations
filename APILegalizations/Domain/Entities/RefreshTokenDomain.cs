using System;
using System.Collections.Generic;

namespace JWT.Data.Models;

public partial class RefreshTokenDomain
{
    public string Token { get; set; } = null!;

    public DateTime Expires { get; set; }

    public DateTime Created { get; set; }

    public DateTime? Revoked { get; set; }

    public int? UserId { get; set; }

}
