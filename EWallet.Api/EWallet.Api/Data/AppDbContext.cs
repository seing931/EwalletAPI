using EWallet.Api.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EWallet.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<ProfileLog> ProfilesLogs { get; set; }
        public DbSet<TopupTransaction> TopupTransactions { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.IdentificationId)
                .IsUnique();

            modelBuilder.Entity<TopupTransaction>()
                .HasIndex(t => t.OrderRef)
                .IsUnique();
        }
    }
}
