using System.ComponentModel.DataAnnotations;

namespace RestaurantBooking.DTOs;

public class RegisterDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
    public string? FullName { get; set; }
}

public class LoginDto
{
    [Required] public string Email { get; set; } = string.Empty;
    [Required] public string Password { get; set; } = string.Empty;
}

public class BookingDto
{
    public Guid TableId { get; set; }
    public DateTime From { get; set; }
    public DateTime To { get; set; }
    public int Guests { get; set; }
    public string? Notes { get; set; }
}

public class RestaurantDto
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
}

public class TableDto
{
    public string Name { get; set; } = string.Empty;
    public int Seats { get; set; }
    public Guid RestaurantId { get; set; }
}

public class MenuItemDto
{
    public string Name { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public Guid RestaurantId { get; set; }
}
