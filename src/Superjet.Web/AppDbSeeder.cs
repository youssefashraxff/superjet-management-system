using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;
using Microsoft.EntityFrameworkCore;

public class AppDbSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Routes.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Routes.csv");
            var format = "yyyy-MM-dd HH:mm";

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var route = new Route_travel
                {
                    Id=int.Parse(parts[0]),
                    Origin = parts[1],
                    Destination = parts[2],
                    DepartureTime = DateTime.ParseExact(parts[3],format,CultureInfo.InvariantCulture),
                    ArrivalTime = DateTime.ParseExact(parts[4],format,CultureInfo.InvariantCulture),
                    Price = decimal.Parse(parts[5]),
                    Distance = 100
                };
                context.Routes.Add(route);
            }
                context.SaveChanges();
        }
        if (!context.Buses.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Buses.csv");

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var bus = new Bus
                {
                    BusNo = parts[1],
                    Model = parts[2],
                    Capacity = int.Parse(parts[3]),
                    Status = Enum.Parse<BusStatus>(parts[4])
                };
                context.Buses.Add(bus);
            }
                context.SaveChanges();
        }
       
        
    }
}