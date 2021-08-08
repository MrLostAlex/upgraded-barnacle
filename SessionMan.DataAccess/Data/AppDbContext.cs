using Microsoft.EntityFrameworkCore;
using SessionMan.DataAccess.Models;

namespace SessionMan.DataAccess.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Client>()
                .HasMany(c => c.Bookings)
                .WithOne(b => b.Client!)
                .HasForeignKey(b => b.ClientId);

            modelBuilder
                .Entity<Session>()
                .HasMany(s => s.Bookings)
                .WithOne(b => b.Session!)
                .HasForeignKey(b => b.SessionId);

            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.Client)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.ClientId);
            
            modelBuilder
                .Entity<Booking>()
                .HasOne(b => b.Session)
                .WithMany(c => c.Bookings)
                .HasForeignKey(b => b.SessionId);
        }
    }
}