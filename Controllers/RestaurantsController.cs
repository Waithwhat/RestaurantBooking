using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RestaurantBooking.DTOs;
using RestaurantBooking.Models;
using RestaurantBooking.Repositories;

namespace RestaurantBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RestaurantsController : ControllerBase
{
    private readonly IRepository<Restaurant> _repo;

    public RestaurantsController(IRepository<Restaurant> repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var list = await _repo.GetAllAsync();
        return Ok(list);
    }

    [HttpPost]
    [Authorize(Roles = "Admin,Manager")]
    public async Task<IActionResult> Create([FromBody] RestaurantDto dto)
    {
        var rest = new Restaurant { Name = dto.Name, Address = dto.Address };
        await _repo.AddAsync(rest);
        await _repo.SaveChangesAsync();
        return CreatedAtAction(nameof(GetById), new { id = rest.Id }, rest);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var r = await _repo.GetAsync(id);
        if (r == null) return NotFound();
        return Ok(r);
    }
}
