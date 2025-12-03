using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;

public class AppDbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.BusRoutes.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/busRoutes.csv");
            var format = "yyyy-MM-dd HH:mm";

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var route = new BusRoute
                {
                    Origin = parts[0],
                    Destination = parts[1],
                    DepartureTime = DateTime.ParseExact(parts[2],format,CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact(parts[3],format,CultureInfo.InvariantCulture),
                    Price = decimal.Parse(parts[4]),
                    Name = "default",
                    Distance = 100
                };
                context.BusRoutes.Add(route);
            }
                context.SaveChanges();
        }
    }
}