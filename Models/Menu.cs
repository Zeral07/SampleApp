using System;
using System.Collections.Generic;

namespace SampleApp.Models;

public partial class Menu
{
    public short Id { get; set; }

    public short? ParentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Url { get; set; }

    public byte? Sequence { get; set; }

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual ICollection<RolesMapping> RolesMappings { get; set; } = new List<RolesMapping>();
}
