using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelListing_API.Data;
using HotelListing_API.Data.Entities;

namespace HotelListing_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Country>>> GetCountries()
    {
        return null;
    }

    // GET: api/Country/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Country>> GetCountriesById(int id)
    {
        return null;
    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, Country country)
    {
        var result = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
        if (result == null)
        {
            return NotFound();
        }
        result.Name = country.Name;
        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!await CountryExistsAsync(id))
            {
                return NotFound();
            }
            return Conflict("Another user modified this record.");
        }
        return NoContent();
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(Country country)
    {
        _context.Countries.Add(country);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetCountriesById), new { id = country.Id }, country);
    }

    // DELETE: api/Country/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        var country = await _context.Countries.FindAsync(id);
        if (country == null)
        {
            return NotFound();
        }

        _context.Countries.Remove(country);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private async Task<bool> CountryExistsAsync(int? id)
    {
        return await _context.Countries.AnyAsync(e => e.Id == id);
    }
}
