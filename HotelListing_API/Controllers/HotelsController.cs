using HotelListing_API.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HotelsController : ControllerBase
{
    private readonly HotelListingDbContext _context;
    public HotelsController(HotelListingDbContext context)
    {
        _context = context;
    }

    // GET: api/Hotels
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels()
    {
        var result = _context.Hotel.ToListAsync();
        if (result == null)
        {
            return NotFound();
        }
        return await _context.Hotel.ToListAsync();
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Hotels>> GetHotels(int id)
    {
        var hotel = await _context.Hotel.Include(q => q.country).FirstOrDefaultAsync(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }

        return Ok(hotel);
    }

    // PUT: api/Hotels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotels(int? id, Hotels hotels)
    {
        if (id != hotels.Id)
        {
            return BadRequest();
        }

        var result = await _context.Hotel.FirstOrDefaultAsync(h => h.Id == id);
        if (result == null)
        {
            return NotFound();
        }

        result.Tilte = hotels.Tilte;
        result.Rating = hotels.Rating;
        result.CountryId = hotels.CountryId;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await HotelsExistsAsync(id))
            {
                return NotFound();
            }
            return Conflict("Another user modified this record");
        }
        return NoContent();
    }

    // POST: api/Hotels
    [HttpPost]
    public async Task<ActionResult<Hotels>> PostHotels(Hotels hotels)
    {
        var check = await _context.Hotel.AnyAsync(h => h.Tilte == hotels.Tilte);
        if (check)
        {
            return BadRequest("Hotel Title already exists!");
        }
        _context.Hotel.Add(hotels);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetHotels", new { id = hotels.Id }, hotels);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotels(int? id)
    {
        var hotels = await _context.Hotel.FindAsync(id);
        if (hotels == null)
        {
            return NotFound();
        }

        _context.Hotel.Remove(hotels);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> HotelsExistsAsync(int? id)
    {
        return await _context.Hotel.AnyAsync(e => e.Id == id);
    }
}
