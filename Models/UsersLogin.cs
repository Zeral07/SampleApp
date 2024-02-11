using System;
using System.Collections.Generic;

namespace SampleApp.Models;

public partial class UsersLogin
{
    public int Id { get; set; }

    public int UsersId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;

    public DateTime? LastLogin { get; set; }

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual User Users { get; set; } = null!;
}
