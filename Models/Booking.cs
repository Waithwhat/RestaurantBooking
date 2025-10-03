using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantBooking.Models;

public class Booking
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid UserId { get; set; }
    public User? User { get; set; }

    public Guid TableId { get; set; }
    public Table? Table { get; set; }

    public DateTime From { get; set; }
    public DateTime To { get; set; }

    public int Guests { get; set; }
    public string? Notes { get; set; }
}
