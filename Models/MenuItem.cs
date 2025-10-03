using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Models;

public class MenuItem
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
    public Restaurant? Restaurant { get; set; }
}
