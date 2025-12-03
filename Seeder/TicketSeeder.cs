using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;

public class TicketSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Tickets.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Ticket.csv");
            var format = "yyyy-MM-dd";

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var ticket = new Ticket
                {
                    SeatNo = parts[1],
                    BookingDate = DateTime.ParseExact(parts[2],format,CultureInfo.InvariantCulture),
                    Status = Enum.Parse<TicketStatus>(parts[3]),
                    RouteId = int.Parse(parts[4]),
                    UserId = int.Parse(parts[5])
                };
                context.Tickets.Add(ticket);
            }
                context.SaveChanges();
        }
    }
}
