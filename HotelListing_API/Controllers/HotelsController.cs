using HotelListing_API.Data;
using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Hotel;
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
    public async Task<ActionResult<IEnumerable<GetHotelsDto>>> GetHotels()
    {
        var search = await _context.Hotel.Select(h => new GetHotelsDto(h.Id,
            h.Title,
            h.Rating,
            h.CountryId)).ToListAsync();
        if (!search.Any())
        {
            return NotFound();
        }
        return Ok(search);
    }

    // GET: api/Hotels/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
    {
        var hotel = await _context.Hotel.FirstOrDefaultAsync(h => h.Id == id);
        if (hotel == null)
        {
            return NotFound();
        }
        GetHotelDto hotelDto = new GetHotelDto(hotel.Id, hotel.Title, hotel.Rating, hotel.CountryId);
        return Ok(hotelDto);
    }

    // PUT: api/Hotels/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutHotels(int id, UpdateHotelDto dto)
    {
        if (id != dto.Id)
        {
            return BadRequest();
        }

        var result = await _context.Hotel.FindAsync(id);
        if (result == null)
        {
            return NotFound();
        }

        result.Title = dto.Title;
        result.Rating = dto.Rating;
        result.CountryId = dto.CountryId;

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
    public async Task<ActionResult<CreateHotelDto>> PostHotels(CreateHotelDto crdto)
    {
        var check = await _context.Hotel.AnyAsync(h => h.Title == crdto.Title);
        if (check)
        {
            return BadRequest("Hotel Title already exists!");
        }
        var dto = new Hotels()
        {
            Title = crdto.Title,
            Rating = crdto.Rating,
            CountryId = crdto.CountryId,
        };
        _context.Hotel.Add(dto);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetHotel), new { id = dto.Id }, dto);
    }

    // DELETE: api/Hotels/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHotels(int id)
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


    [HttpGet("Test")]
    public ActionResult<List<object>> TestQueryString(string rating,int countryid)
    {
        var result = new List<object>();
        result.Add((rating, countryid));

        return result;
    }
    
}
