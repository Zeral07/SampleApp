using System;
using System.Collections.Generic;

namespace SampleApp.Models;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string? Email { get; set; }

    public string? Position { get; set; }

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual ICollection<UsersLogin> UsersLogins { get; set; } = new List<UsersLogin>();

    public virtual ICollection<UsersMapping> UsersMappings { get; set; } = new List<UsersMapping>();
}
