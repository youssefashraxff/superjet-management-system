namespace Superjet.Web.Models
{
    public class Passenger : User
    {
        public string Email { get; set; }
        
        // Inherits: UserId, UserName, Password, Phone, Gender, Tickets
    }
}
