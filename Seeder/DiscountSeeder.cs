using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;

public class DiscountSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Discounts.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/Discount.csv");
            var format = "yyyy-MM-dd";

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var discount = new Discount
                {
                    Code = parts[1],
                    Percentage = decimal.Parse(parts[2]),
                    ValidUntil = DateTime.ParseExact(parts[3],format,CultureInfo.InvariantCulture)
                };
                context.Discounts.Add(discount);
            }
                context.SaveChanges();
        }
    }
}
