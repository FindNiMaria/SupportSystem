using HelpdeskSystem.Models;
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
    public DbSet<TicketCategory> TicketCategories{ get; set; }
    public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
    public DbSet<SystemCode> systemCodes { get; set; }
    public DbSet<SystemCodeDetail> systemCodeDetails { get; set; }
    public DbSet<Department> Departments { get; set; }
    public DbSet<TicketResolution> TicketResolutions { get; set; }
    public DbSet<SystemTask> SystemTasks { get; set; }
    public DbSet<SystemSettings> SystemSettings { get; set; }

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
    }

public DbSet<HelpdeskSystem.Models.Comment> Comment_1 { get; set; } = default!;

}
