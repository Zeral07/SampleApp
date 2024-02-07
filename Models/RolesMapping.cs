namespace SampleApp.Models;

public partial class RolesMapping
{
    public int Id { get; set; }

    public short RolesId { get; set; }

    public short MenusId { get; set; }

    public bool IsView { get; set; }

    public bool IsEdit { get; set; }

    public short RowStatus { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTime { get; set; }

    public string? LastUpdatedBy { get; set; }

    public DateTime? LastUpdatedTime { get; set; }

    public virtual Menu Menus { get; set; } = null!;

    public virtual Role Roles { get; set; } = null!;
}
