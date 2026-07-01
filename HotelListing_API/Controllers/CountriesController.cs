using HotelListing_API.Contracts;
using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;
using System.Diagnostics.Metrics;

namespace HotelListing_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CountriesController : ControllerBase
{
    private readonly ICountriesService _countriesService;

    public CountriesController(ICountriesService countriesService)
    {
        _countriesService = countriesService;
    }

    // GET: api/Country
    [HttpGet]
    public async Task<ActionResult<IEnumerable<GetCountriesDto>>> GetCountries()
    {
        return Ok(await _countriesService.GetCountries());
    }

    // GET: api/Country/5
    [HttpGet("{id}")]
    public async Task<ActionResult<GetCountryDto>> GetCountriesById(int id)
    {
        try
        {
            return Ok(await _countriesService.GetCountryById(id));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }

    }

    // PUT: api/Country/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCountry(int id, UpdateCountryDto country)
    {
        try
        {
            await _countriesService.UpdateCountry(id, country);
            return NoContent();
        }
        catch(KeyNotFoundException)
        {
            return NotFound();
        }
        catch(DbUpdateConcurrencyException)
        {
            return Conflict("Record was modified by another user");
        }
    }

    // POST: api/Country
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Country>> PostCountry(CreateCountryDto newcountry)
    {
        try
        {
            await _countriesService.CreateCountry(newcountry);
            return Ok(newcountry);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
        catch(InvalidOperationException ex)
        {
            return Conflict(ex.Message);
        }
    }

    // DELETE: api/Country/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCountry(int id)
    {
        try
        {
            await _countriesService.DeleteCountry(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
    }
}
