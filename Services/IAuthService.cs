using RestaurantBooking.DTOs;

namespace RestaurantBooking.Services;

public interface IAuthService
{
    Task<string> RegisterAsync(RegisterDto dto);
    Task<string?> LoginAsync(LoginDto dto);
}
