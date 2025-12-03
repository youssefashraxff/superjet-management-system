using Microsoft.EntityFrameworkCore;
using Superjet.Web.Models; 

namespace Superjet.Web.Data 
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {}

        public DbSet<Bus> Buses { get; set; }
        public DbSet<BusRoute> BusRoutes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-Many: Bus <-> BusRoute
            modelBuilder.Entity<Bus>()
                .HasMany(b => b.BusRoutes)
                .WithMany(r => r.Buses)
                .UsingEntity(j => j.ToTable("BusesRoutes"));

            // One-to-Many: BusRoute -> Ticket
            modelBuilder.Entity<BusRoute>()
                .HasMany(r => r.Tickets)
                .WithOne(t => t.BusRoute)
                .HasForeignKey(t => t.Id)
                .OnDelete(DeleteBehavior.Cascade);

            // One-to-Many: User -> Ticket
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tickets)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // One-to-Many (optional): Discount -> Ticket
            modelBuilder.Entity<Discount>()
                .HasMany(d => d.Tickets)
                .WithOne(t => t.discount)
                .HasForeignKey(t => t.Id)
                .OnDelete(DeleteBehavior.SetNull);

            base.OnModelCreating(modelBuilder);
        }
    }
}