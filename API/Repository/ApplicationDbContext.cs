using Microsoft.EntityFrameworkCore;
using PublicTimeAPI.Models;
using System;

namespace PublicTimeAPI.Repository
{
  public class ApplicationDbContext : DbContext
  {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    { }
    public DbSet<UserData> UserDatas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);
      if (modelBuilder == null)
      {
        throw new ArgumentNullException(nameof(modelBuilder));
      }

      modelBuilder.Entity<UserData>(e =>
      {
        e.HasKey(o => o.Id);
        e.Property(o => o.Id).ValueGeneratedOnAdd();
        e.HasIndex(o => o.ClientId).IsUnique();
        e.HasIndex(o => o.Email).IsUnique();
      });
    }
  }
}
