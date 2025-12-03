using Superjet.Web.Models;
using Superjet.Web.Data;
using System.Globalization;

public class UserSeeder
{
    public static void Seed(AppDbContext context)
    {
        if (!context.Users.Any())
        {
            var lines = File.ReadAllLines("Data/Seed/User.csv");

            foreach(var line in lines.Skip(1))
            {
                var parts = line.Split(',');
                var user = new User
                {
                    UserName = parts[1],
                    Password = parts[2],
                    Phone = parts[3],
                    Gender = Enum.Parse<Gender>(parts[4])
                };
                context.Users.Add(user);
            }
                context.SaveChanges();
        }
    }
}
