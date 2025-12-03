using Superjet.Web.Models;
using Superjet.Web.Data;

public class AppDbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.BusRoutes.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/busRoutes.csv");
            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var route = new BusRoute
                {
                    Origin = parts[0],
                    Destination = parts[1],
                    DepartureTime = DateTime.Today.Add(TimeSpan.Parse(parts[2])),
                    ArrivalTime = DateTime.Today.Add(TimeSpan.Parse(parts[3])),
                    Price = decimal.Parse(parts[4])
                };
                context.BusRoutes.Add(route);
            }
                context.SaveChanges();
        }
    }
}