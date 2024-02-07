using Microsoft.EntityFrameworkCore;

namespace SampleApp.Models;

public partial class SampleDbContext : DbContext
{
    public SampleDbContext()
    {
    }

    public SampleDbContext(DbContextOptions<SampleDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Menu> Menus { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RolesMapping> RolesMappings { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersLogin> UsersLogins { get; set; }

    public virtual DbSet<UsersMapping> UsersMappings { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Menu>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Menus__3214EC279DE0E1DC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ParentId).HasColumnName("ParentID");
            entity.Property(e => e.Url)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("URL");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Roles__3214EC27BA89D520");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RolesMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__RolesMap__3214EC27B8420A68");

            entity.ToTable("RolesMapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.MenusId).HasColumnName("MenusID");
            entity.Property(e => e.RolesId).HasColumnName("RolesID");

            entity.HasOne(d => d.Menus).WithMany(p => p.RolesMappings)
                .HasForeignKey(d => d.MenusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolesMapp__Menus__412EB0B6");

            entity.HasOne(d => d.Roles).WithMany(p => p.RolesMappings)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RolesMapp__Roles__403A8C7D");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC273425212D");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.Position)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsersLogin>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersLog__3214EC27A0EEC4C5");

            entity.ToTable("UsersLogin");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.Password)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.RefreshToken)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UsersId).HasColumnName("UsersID");

            entity.HasOne(d => d.Users).WithMany(p => p.UsersLogins)
                .HasForeignKey(d => d.UsersId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersLogi__Users__4222D4EF");
        });

        modelBuilder.Entity<UsersMapping>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__UsersMap__3214EC27ACF1E005");

            entity.ToTable("UsersMapping");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedTime).HasColumnType("datetime");
            entity.Property(e => e.LastUpdatedBy)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastUpdatedTime).HasColumnType("datetime");
            entity.Property(e => e.RolesId).HasColumnName("RolesID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Roles).WithMany(p => p.UsersMappings)
                .HasForeignKey(d => d.RolesId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersMapp__Roles__440B1D61");

            entity.HasOne(d => d.User).WithMany(p => p.UsersMappings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__UsersMapp__UserI__4316F928");
        });
    }
}
