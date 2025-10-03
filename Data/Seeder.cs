using Microsoft.AspNetCore.Identity;
using RestaurantBooking.Models;
using RestaurantBooking.Repositories;

namespace RestaurantBooking.Data;

public static class DataSeeder
{
    public static async Task SeedAsync(AppDbContext ctx, IPasswordHasher<User> hasher)
    {
        if (ctx.Users.Any()) return; // already seeded

        var admin = new User
        {
            Email = "admin@restaurant.com",
            FullName = "Admin User",
            Role = "Admin"
        };
        admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");

        await ctx.Users.AddAsync(admin);

        var rest = new Restaurant
        {
            Name = "Central Bistro",
            Address = "123 Main St"
        };
        await ctx.Restaurants.AddAsync(rest);
        await ctx.SaveChangesAsync();

        var t1 = new Table { Name = "T1", Seats = 4, RestaurantId = rest.Id };
        var t2 = new Table { Name = "T2", Seats = 2, RestaurantId = rest.Id };
        await ctx.Tables.AddRangeAsync(t1, t2);

        var m1 = new MenuItem { Name = "Margherita Pizza", Price = 9.99m, RestaurantId = rest.Id };
        var m2 = new MenuItem { Name = "Caesar Salad", Price = 6.50m, RestaurantId = rest.Id };
        await ctx.MenuItems.AddRangeAsync(m1, m2);

        await ctx.SaveChangesAsync();
    }
}
