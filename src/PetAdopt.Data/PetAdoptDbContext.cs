using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PetAdopt.Data.Identity;
using PetAdopt.Domain.Entities;

namespace PetAdopt.Data;

/// <summary>EF Core DbContext for PetAdopt.</summary>
public class PetAdoptDbContext : IdentityDbContext<ApplicationUser>
{
    public PetAdoptDbContext(DbContextOptions<PetAdoptDbContext> options) : base(options) { }

    public DbSet<Shelter> Shelters => Set<Shelter>();
    public DbSet<Pet> Pets => Set<Pet>();
    public DbSet<AdoptionApplication> Applications => Set<AdoptionApplication>();
    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder b)
    {
        base.OnModelCreating(b);

        b.Entity<Shelter>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(150).IsRequired();
            e.HasMany(x => x.Pets).WithOne(x => x.Shelter).HasForeignKey(x => x.ShelterId);
            e.HasMany(x => x.Reviews).WithOne(x => x.Shelter).HasForeignKey(x => x.ShelterId);
        });

        b.Entity<Pet>(e =>
        {
            e.Property(x => x.Name).HasMaxLength(100).IsRequired();
            e.Property(x => x.Status).HasConversion<int>();
            e.Property(x => x.Species).HasConversion<int>();
        });

        b.Entity<AdoptionApplication>(e =>
        {
            e.Property(x => x.ApplicantUserId).IsRequired();
            e.Property(x => x.Status).HasConversion<int>();
        });

        b.Entity<Review>(e =>
        {
            e.Property(x => x.Rating).IsRequired();
        });
    }
}
