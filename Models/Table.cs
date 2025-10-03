using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBooking.Models;

public class Table
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public int Seats { get; set; }
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
    public List<Booking>? Bookings { get; set; }
}
