using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantBooking.DTOs;
using RestaurantBooking.Models;
using RestaurantBooking.Repositories;
using System.Security.Claims;

namespace RestaurantBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _repo;

    public BookingsController(IBookingRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> Create([FromBody] BookingDto dto)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var booking = new Booking
        {
            TableId = dto.TableId,
            From = dto.From,
            To = dto.To,
            Guests = dto.Guests,
            Notes = dto.Notes,
            UserId = Guid.Parse(userId)
        };

        // check conflicts
        var conflicts = await _repo.GetBookingsForTable(dto.TableId, dto.From, dto.To);
        if (conflicts.Any()) return BadRequest(new { error = "Time slot is already booked." });

        await _repo.AddAsync(booking);
        await _repo.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = booking.Id }, booking);
    }

    [HttpGet("{id}")]
    [Authorize]
    public async Task<IActionResult> GetById(Guid id)
    {
        var b = await _repo.GetAsync(id);
        if (b == null) return NotFound();
        return Ok(b);
    }

    [HttpGet("user/me")]
    [Authorize]
    public async Task<IActionResult> MyBookings()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();
        var list = await _repo.GetBookingsForUser(Guid.Parse(userId));
        return Ok(list);
    }
}
