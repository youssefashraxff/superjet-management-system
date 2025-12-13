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
        public DbSet<Route_travel> Routes { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    // One-to-Many: Bus -> Route_travel
    modelBuilder.Entity<Bus>()
        .HasMany(r=> r.Routes)
        .WithOne(b => b.Bus)
        .HasForeignKey(b=>b.BusId);

    // One-to-Many: Route_travel -> Ticket
    modelBuilder.Entity<Route_travel>()
        .HasMany(t => t.Tickets)
        .WithOne(r => r.Route)
        .HasForeignKey(r => r.RouteId);

    // One-to-Many: User -> Ticket
    modelBuilder.Entity<User>()
        .HasMany(u => u.Tickets)
        .WithOne(t => t.User)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Restrict);

    // One-to-Many (optional): Discount -> Ticket
    modelBuilder.Entity<Discount>()
        .HasMany(d => d.Tickets)
        .WithOne(t => t.Discount)
        .HasForeignKey(t => t.DiscountId)
        .OnDelete(DeleteBehavior.SetNull);

    modelBuilder.Entity<Cart>()
    .HasMany(c => c.Items)
    .WithOne(i => i.Cart)
    .HasForeignKey(i => i.CartId)
    .OnDelete(DeleteBehavior.Cascade);

    modelBuilder.Entity<Cart>()
    .HasOne(c => c.Discount)
    .WithMany(d => d.Carts)
    .HasForeignKey(c => c.DiscountId)
    .OnDelete(DeleteBehavior.SetNull);

    base.OnModelCreating(modelBuilder);
}
    }
}