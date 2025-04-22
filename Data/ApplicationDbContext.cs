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
    public DbSet<OS> OS { get; set; }
    public DbSet<OSComments> OSComments { get; set; }
    public DbSet<TicketCategory> TicketCategories{ get; set; }
    public DbSet<TicketSubCategory> TicketSubCategories { get; set; }
    public DbSet<OSCategory> OSCategories { get; set; }
    public DbSet<SystemCode> systemCodes { get; set; }
    public DbSet<SystemCodeDetail> systemCodeDetails { get; set; }
    public DbSet<Department> Departments { get; set; }

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

        builder.Entity<SystemCodeDetail>()
            .HasOne(c => c.SystemCode)
            .WithMany()
            .HasForeignKey(c => c.SystemCodeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
            .HasOne(c => c.Prioridade)
            .WithMany()
            .HasForeignKey(c => c.PrioridadeId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
            .HasOne(c => c.Status)
            .WithMany()
            .HasForeignKey(c => c.StatusId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<TicketCategory>()
        .HasOne(c => c.PrioridadePadrao)
        .WithMany()
        .HasForeignKey(c => c.PrioridadePadraoId)
        .OnDelete(DeleteBehavior.Restrict);
        builder.Entity<Ticket>()
        .HasOne(t => t.Categoria)
        .WithMany()
        .HasForeignKey(t => t.CategoriaId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<TicketCategory>()
            .HasOne(tc => tc.PrioridadePadrao)
            .WithMany()
            .HasForeignKey(tc => tc.PrioridadePadraoId)
            .OnDelete(DeleteBehavior.Restrict);
    }

public DbSet<HelpdeskSystem.Models.Comment> Comment_1 { get; set; } = default!;

public DbSet<HelpdeskSystem.Models.OSComments> OSComments_1 { get; set; } = default!;
}
