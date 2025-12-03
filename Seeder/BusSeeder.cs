using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;

public class BusSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Buses.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Bus.csv");

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
