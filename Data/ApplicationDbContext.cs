using HelpdeskSystem.Models.SO;
using HelpdeskSystem.Models.System;
using HelpdeskSystem.Models.Ticket;
using HelpdeskSystem.Models.User;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace HelpdeskSystem.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    public DbSet<Ticket> Tickets { get; set; }
    public DbSet<Comment> Comment { get; set; }
    public DbSet<AuditTrail> AuditTrails { get; set; }
    public DbSet<TicketCategory> TicketCategories { get; set; }
    public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
    public DbSet<SystemCode> systemCodes { get; set; }
    public DbSet<SystemCodeDetail> systemCodeDetails { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<TicketResolution> TicketResolutions { get; set; }
    public DbSet<OSResolution> OSResolutions { get; set; }
    public DbSet<SystemTask> SystemTasks { get; set; }
    public DbSet<SystemSettings> SystemSettings { get; set; }
    public DbSet<OS> OS { get; set; }
    public DbSet<OSComment> OSComment { get; set; }
    public DbSet<OSCategory> OSCategories { get; set; }
    public DbSet<OSSubCategory> OSSubCategories { get; set; }

    // Novas entidades para permissionamento
    public DbSet<Permission> Permissions { get; set; }
    public DbSet<RolePermission> RolePermissions { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    public DbSet<UserPermission> UserPermissions { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany(c => c.TicketComments)
            .HasForeignKey(c => c.TicketId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(c => c.ModifiedBy)
            .WithMany()
            .HasForeignKey(c => c.ModifiedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SystemCodeDetail>()
            .HasOne(c => c.SystemCode)
            .WithMany()
            .HasForeignKey(c => c.SystemCodeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
            .HasOne(c => c.Priority)
            .WithMany()
            .HasForeignKey(c => c.PriorityId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<TicketCategory>()
        .HasOne(c => c.DefautPriority)
        .WithMany()
        .HasForeignKey(c => c.DefaultPriorityId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
        .HasOne(t => t.Category)
        .WithMany()
        .HasForeignKey(t => t.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(tc => tc.DefautPriority)
            .WithMany()
            .HasForeignKey(tc => tc.DefaultPriorityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketResolution>()
            .HasOne(tr => tr.Status)
            .WithMany()
            .HasForeignKey(tr => tr.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<SystemTask>()
            .HasOne(tr => tr.Parent)
            .WithMany()
            .HasForeignKey(tr => tr.ParentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OS>()
            .HasOne(tr => tr.Priority)
            .WithMany()
            .HasForeignKey(tr => tr.PriorityId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OS>()
            .HasOne(c => c.CreatedBy)
            .WithMany()
            .HasForeignKey(c => c.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OS>()
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OS>()
        .HasOne(t => t.Category)
        .WithMany()
        .HasForeignKey(t => t.CategoryId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Permission>()
                .HasIndex(p => p.Code)
                .IsUnique();

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Role)
            .WithMany(r => r.RolePermissions)
            .HasForeignKey(rp => rp.RoleId);

        builder.Entity<RolePermission>()
            .HasOne(rp => rp.Permission)
            .WithMany()
            .HasForeignKey(rp => rp.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany()
            .HasForeignKey(ur => ur.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany()
            .HasForeignKey(ur => ur.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserRole>()
            .HasOne(ur => ur.Department)
            .WithMany()
            .HasForeignKey(ur => ur.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserPermission>()
            .HasOne(up => up.User)
            .WithMany()
            .HasForeignKey(up => up.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<UserPermission>()
            .HasOne(up => up.Permission)
            .WithMany()
            .HasForeignKey(up => up.PermissionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Adicionar índices para melhorar a performance
        builder.Entity<UserRole>()
            .HasIndex(ur => new { ur.UserId, ur.RoleId, ur.DepartmentId })
            .IsUnique();

        builder.Entity<UserPermission>()
            .HasIndex(up => new { up.UserId, up.PermissionId })
            .IsUnique();

        builder.Entity<RolePermission>()
            .HasIndex(rp => new { rp.RoleId, rp.PermissionId })
            .IsUnique();
    }

    public DbSet<Models.Ticket.Comment> Comment_1 { get; set; } = default!;
    public DbSet<Models.SO.OSComment> OSComment_1 { get; set; } = default!;

}