using HotelListing_API.Contracts;
using HotelListing_API.Data;
using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Country;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HotelListing_API.Services
{
    public class CountriesService : ICountriesService
    {
        private readonly HotelListingDbContext _context;
        public CountriesService(HotelListingDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Country>?> GetCountries()
        {
            return await _context.Countries.ToListAsync();
        }

        public async Task<GetCountryDto> GetCountrغById(int id)
        {
            var result = await _context.Countries.Where(c => c.Id == id).Select(c => new GetCountryDto(c.Id, c.Name))
                 .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new KeyNotFoundException("This record not found!");
            }
            return result;
        }

        public async Task PutCountry(int id, UpdateCountryDto upcountrydto)
        {
            var result = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new KeyNotFoundException("Country not found!!");
            }
            result.Name = upcountrydto.Name;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExistsAsync(id))
                {
                    throw new InvalidOperationException("This record not found!");
                }
                throw new InvalidOperationException("Another user modified this record.");
            }
        }


        public async Task DeleteCountry(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                throw new KeyNotFoundException("Country not found!!");
            }

            _context.Countries.Remove(country);
            await _context.SaveChangesAsync();
        }

        private async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Countries.AnyAsync(c => c.Id == id);
        }
    }
}
