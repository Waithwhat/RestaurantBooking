using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using RestaurantBooking.DTOs;
using RestaurantBooking.Models;
using RestaurantBooking.Repositories;

namespace RestaurantBooking.Services;

public class AuthService : IAuthService
{
    private readonly IRepository<User> _userRepo;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IConfiguration _configuration;

    public AuthService(IRepository<User> userRepo, IPasswordHasher<User> passwordHasher, IConfiguration configuration)
    {
        _userRepo = userRepo;
        _passwordHasher = passwordHasher;
        _configuration = configuration;
    }

    public async Task<string> RegisterAsync(RegisterDto dto)
    {
        // basic check
        var exists = await _userRepo.FindAsync(u => u.Email == dto.Email);
        if (exists != null)
            throw new Exception("Email already registered.");

        var user = new User { Email = dto.Email, FullName = dto.FullName, Role = "User" };
        user.PasswordHash = _passwordHasher.HashPassword(user, dto.Password);

        await _userRepo.AddAsync(user);
        await _userRepo.SaveChangesAsync();

        return "ok";
    }

    public async Task<string?> LoginAsync(LoginDto dto)
    {
        var user = await _userRepo.FindAsync(u => u.Email == dto.Email);
        if (user == null) return null;

        var result = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, dto.Password);
        if (result == PasswordVerificationResult.Failed) return null;

        // create jwt
        var jwtSection = _configuration.GetSection("JwtSettings");
        var secret = jwtSection.GetValue<string>("Secret") ?? throw new Exception("Jwt secret missing");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.Email),
            new Claim(ClaimTypes.Role, user.Role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSection.GetValue<string>("Issuer"),
            audience: jwtSection.GetValue<string>("Audience"),
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSection.GetValue<int>("ExpiryMinutes")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
