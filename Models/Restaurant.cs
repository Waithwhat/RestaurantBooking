using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.Models;

public class Restaurant
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public List<Table>? Tables { get; set; }
    public List<MenuItem>? Menu { get; set; }
}
