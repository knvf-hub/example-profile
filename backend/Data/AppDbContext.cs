using backend.Models;
using Microsoft.EntityFrameworkCore;

namespace backend.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Profile> Profiles => Set<Profile>();
    public DbSet<Occupation> Occupations => Set<Occupation>();
    public DbSet<ProfileOccupation> ProfileOccupations => Set<ProfileOccupation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProfileOccupation>()
            .HasKey(x => new { x.ProfileId, x.OccupationId });

        modelBuilder.Entity<ProfileOccupation>()
            .HasOne(x => x.Profile)
            .WithMany(x => x.ProfileOccupations)
            .HasForeignKey(x => x.ProfileId);

        modelBuilder.Entity<ProfileOccupation>()
            .HasOne(x => x.Occupation)
            .WithMany(x => x.ProfileOccupations)
            .HasForeignKey(x => x.OccupationId);

        modelBuilder.Entity<Occupation>().HasData(
            new Occupation { Id = 1, Name = "โปรแกรมเมอร์" },
            new Occupation { Id = 2, Name = "ดีไซน์เนอร์" },
            new Occupation { Id = 3, Name = "ครู" },
            new Occupation { Id = 4, Name = "หมอ" },
            new Occupation { Id = 5, Name = "วิศวกร" },
            new Occupation { Id = 6, Name = "นักบัญชี" },
            new Occupation { Id = 7, Name = "นักการตลาด" },
            new Occupation { Id = 8, Name = "นักเขียน" },
            new Occupation { Id = 9, Name = "นักวิทยาศาสตร์" },
            new Occupation { Id = 10, Name = "นักกฎหมาย" }
        );
    }


}