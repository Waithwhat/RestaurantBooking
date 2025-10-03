using RestaurantBooking.Models;

namespace RestaurantBooking.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IEnumerable<Booking>> GetBookingsForTable(Guid tableId, DateTime from, DateTime to);
    Task<IEnumerable<Booking>> GetBookingsForUser(Guid userId);
}
