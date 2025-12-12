using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

public class AppDbSeeder
{
    public static void Seed(AppDbContext context)
    {
        // --- SEED BUSES ---
        if (!context.Buses.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Buses.csv");

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                var bus = new Bus
                {
                    Id = int.Parse(parts[0]),
                    BusNo = parts[1],
                    Model = parts[2],
                    Capacity = int.Parse(parts[3]),
                    Status = Enum.Parse<BusStatus>(parts[4])
                };

                context.Buses.Add(bus);
            }

            context.SaveChanges();
        }

        // --- SEED ROUTES ---
        if (!context.Routes.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Routes.csv");
            var format = "yyyy-MM-dd HH:mm";

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                var route = new Route_travel
                {
                    Id = int.Parse(parts[0]),
                    Origin = parts[1],
                    Destination = parts[2],
                    Distance = decimal.Parse(parts[3]),
                    DepartureTime = DateTime.ParseExact(parts[4], format, CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact(parts[5], format, CultureInfo.InvariantCulture),
                    Price = decimal.Parse(parts[6]),
                    BusId = int.Parse(parts[7])
                };

                context.Routes.Add(route);
            }

            context.SaveChanges();
        }

        // --- SEED USERS ---
        if (!context.Users.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Users.csv");

            foreach (var line in lines.Skip(1))
            {
                var parts = line.Split(',');

                var user = new User
                {
                    Id = int.Parse(parts[0]),
                    UserName = parts[1],
                    Password = parts[2]
                };

                context.Users.Add(user);
            }

            context.SaveChanges();
        }

        // --- SEED Tickets ---
        // --- SEED Tickets (robust) ---
if (!context.Tickets.Any())
{
    var lines = File.ReadAllLines("Data/Seed/Tickets.csv");
    var format = "yyyy-MM-dd HH:mm";
    var lineNo = 1;

    foreach (var raw in lines.Skip(1))
    {
        lineNo++;
        var line = raw?.Trim();
        if (string.IsNullOrWhiteSpace(line)) continue;

        var parts = line.Split(',');

        // Quick validation: must have >= 7 columns
        if (parts.Length < 7)
        {
            Console.WriteLine($"[Tickets Seeder] Skipping line {lineNo}: not enough columns -> '{line}'");
            continue;
        }

        // trim fields
        for (int i = 0; i < parts.Length; i++) parts[i] = parts[i].Trim();

        try
        {
            var id = int.Parse(parts[0]);
            var seatNo = parts[1];
            var bookingDate = DateTime.ParseExact(parts[2], format, CultureInfo.InvariantCulture);
            var status = Enum.Parse<TicketStatus>(parts[3]);
            var userId = int.Parse(parts[4]);
            var routeId = int.Parse(parts[5]);

            int? discountId = null;
            if (!string.IsNullOrWhiteSpace(parts[6]) && parts[6] != "0")
                discountId = int.Parse(parts[6]);

            // Validate FKs BEFORE adding to context
            if (!context.Users.Any(u => u.Id == userId))
            {
                Console.WriteLine($"[Tickets Seeder] Skipping line {lineNo}: UserId {userId} NOT FOUND -> '{line}'");
                continue;
            }

            if (!context.Routes.Any(r => r.Id == routeId))
            {
                Console.WriteLine($"[Tickets Seeder] Skipping line {lineNo}: RouteId {routeId} NOT FOUND -> '{line}'");
                continue;
            }

            if (discountId.HasValue && !context.Discounts.Any(d => d.Id == discountId.Value))
            {
                Console.WriteLine($"[Tickets Seeder] Warning line {lineNo}: DiscountId {discountId} NOT FOUND, setting to null -> '{line}'");
                discountId = null;
            }

            var ticket = new Ticket
            {
                Id = id,
                SeatNo = seatNo,
                BookingDate = bookingDate,
                Status = status,
                UserId = userId,
                RouteId = routeId,
                DiscountId = discountId
            };

            context.Tickets.Add(ticket);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[Tickets Seeder] ERROR parsing line {lineNo}: '{line}'");
            Console.WriteLine($"[Tickets Seeder] Exception: {ex.GetType().Name} - {ex.Message}");
            // skip this row and continue
            continue;
        }
    }

    try
    {
        context.SaveChanges();
        Console.WriteLine("[Tickets Seeder] Completed.");
    }
    catch (Exception ex)
    {
        Console.WriteLine($"[Tickets Seeder] SaveChanges FAILED: {ex.GetType().Name} - {ex.Message}");
        throw;
    }
}

    }
}