using System;
using System.Collections.Generic;

namespace SampleApp.Models;

public partial class Role
{
    public short Id { get; set; }

    public string Name { get; set; } = null!;

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual ICollection<RolesMapping> RolesMappings { get; set; } = new List<RolesMapping>();

    public virtual ICollection<UsersMapping> UsersMappings { get; set; } = new List<UsersMapping>();
}
