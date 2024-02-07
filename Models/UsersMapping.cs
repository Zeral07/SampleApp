using System;
using System.Collections.Generic;

namespace SampleApp.Models;

public partial class UsersMapping
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public short RolesId { get; set; }

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual Role Roles { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
