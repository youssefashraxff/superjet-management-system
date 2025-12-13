namespace Superjet.Web.Models
{
public class BusCreateDto
{
    public int Id { get; set; }   // <-- Required for editing
    public string BusNo { get; set; }
    public string Model { get; set; }
    public int Capacity { get; set; }
    public string Status { get; set; }
}
}