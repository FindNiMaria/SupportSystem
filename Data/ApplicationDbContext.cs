using HelpdeskSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

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
    public DbSet<OS> OS { get; set; }
    public DbSet<OSComments> OSComments { get; set; }
    public DbSet<TicketCategory> TicketCategories{ get; set; }
    public DbSet<OSCategory> OSCategories { get; set; }
    public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
    public DbSet<OSSubCategory> OSSubCategories { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Ticket>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OS>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.Ticket)
            .WithMany()
            .HasForeignKey(c => c.IdChamado)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Comment>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OSComments>()
           .HasOne(c => c.OS)
           .WithMany()
           .HasForeignKey(c => c.IdOS)
           .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OSComments>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(c => c.ModificadoPor)
            .WithMany()
            .HasForeignKey(c => c.ModificadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OSCategory>()
            .HasOne(c => c.ModificadoPor)
            .WithMany()
            .HasForeignKey(c => c.ModificadoPorId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<OSCategory>()
            .HasOne(c => c.CriadoPor)
            .WithMany()
            .HasForeignKey(c => c.CriadoPorId)
            .OnDelete(DeleteBehavior.Restrict);
    }

public DbSet<HelpdeskSystem.Models.Comment> Comment_1 { get; set; } = default!;

public DbSet<HelpdeskSystem.Models.OSComments> OSComments_1 { get; set; } = default!;
}
