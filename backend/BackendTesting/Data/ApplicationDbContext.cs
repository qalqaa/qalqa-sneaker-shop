using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using qalqasneakershop.Data.Identity;
using qalqasneakershop.Models;
using Autorisation.Data;
using System.Text.Json;
using System.Reflection.Emit;

namespace qalqasneakershop.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationIdentityUser>
    {
        public DbSet<Item> Items { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Item>()
                .Property(i => i.Description)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true }),
                    v => JsonSerializer.Deserialize<ItemDescription>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
            modelBuilder.Entity<Item>()
                .Property(i => i.Reviews)
                .HasConversion(
                    v => JsonSerializer.Serialize(v, new JsonSerializerOptions { WriteIndented = true}),
                    v => JsonSerializer.Deserialize<List<ItemReviews>>(v, new JsonSerializerOptions { PropertyNameCaseInsensitive = true }));
        }
    }
    public class ApplicationUserDbContext : DbContext
    {
        public ApplicationUserDbContext(DbContextOptions<ApplicationUserDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> UsersFull { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("UsersFull");
                entity.HasKey(e => e.UserID);
            });

            modelBuilder.Entity<Order>()
            .Property(o => o.OrderID)
            .ValueGeneratedOnAdd();
        }
    }

}
