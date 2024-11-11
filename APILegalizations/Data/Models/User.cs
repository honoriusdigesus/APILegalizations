﻿using System;
using System.Collections.Generic;

namespace APILegalizations.Data.Models;

public partial class User
{
    public int UserId { get; set; }

    public int IdentityDocument { get; set; }

    public string Username { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
