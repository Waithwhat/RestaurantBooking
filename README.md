RestaurantBooking - ASP.NET Core 8 project skeleton
This project includes:
- Models: User, Restaurant, Table, Booking, MenuItem
- DbContext: AppDbContext
- Repositories: generic IRepository + BookingRepository
- Auth service with JWT (simple implementation)
- Controllers: Auth, Restaurants, Bookings, Users
- DTOs for input models

Instructions:
1. Open the solution folder in Visual Studio or VSCode with C# extension.
2. Run `dotnet restore` to install packages.
3. Adjust the connection string in appsettings.json (Default uses LocalDB).
4. Run `dotnet ef migrations add Initial` and `dotnet ef database update` to create DB.
5. Run the project `dotnet run`.

Note: This is a skeleton intended to be a working starting point. For production, change the JWT secret, add validation, better error handling, refresh tokens, and use Identity if needed.


Seed & Admin

An initial admin user is created during startup (if database empty):

- Email: admin@restaurant.com
- Password: Admin123!

The application runs EF Migrations automatically on startup via Database.Migrate().
