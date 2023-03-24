using System;
using System.Collections.Generic;

namespace CI_PLATFORM.Entities.DataModels;

public partial class PasswordReset
{
    public string Email { get; set; } = null!;

    public string Token { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public long Id { get; set; }
}
