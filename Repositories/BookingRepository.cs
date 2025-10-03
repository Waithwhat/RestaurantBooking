using Microsoft.EntityFrameworkCore;
using RestaurantBooking.Data;
using RestaurantBooking.Models;

namespace RestaurantBooking.Repositories;

public class BookingRepository : Repository<Booking>, IBookingRepository
{
    public BookingRepository(AppDbContext ctx) : base(ctx) { }

    public async Task<IEnumerable<Booking>> GetBookingsForTable(Guid tableId, DateTime from, DateTime to)
    {
        return await _dbSet
            .Where(b => b.TableId == tableId && b.From < to && b.To > from)
            .ToListAsync();
    }

    public async Task<IEnumerable<Booking>> GetBookingsForUser(Guid userId)
    {
        return await _dbSet.Where(b => b.UserId == userId).ToListAsync();
    }
}
