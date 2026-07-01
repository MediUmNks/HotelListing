using HotelListing_API.Contracts;
using HotelListing_API.Data;
using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Country;
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
        public async Task<IEnumerable<GetCountriesDto>?> GetCountries()
        {
            return await _context.Countries.Select(c => new GetCountriesDto(c.Id, c.Name))
                .ToListAsync();
        }

        public async Task<GetCountryDto> GetCountryById(int id)
        {
            var result = await _context.Countries.Where(c => c.Id == id).Select(c => new GetCountryDto(c.Id, c.Name))
                 .FirstOrDefaultAsync();

            if (result == null)
            {
                throw new KeyNotFoundException("Record not found!");
            }
            return result;
        }

        public async Task<Country> CreateCountry(CreateCountryDto createcountry)
        {
            if (string.IsNullOrWhiteSpace(createcountry.Name))
                throw new ArgumentException("Name Is required");
            var check = await _context.Countries.AnyAsync(c => c.Name == createcountry.Name);
            if (check)
            {
                throw new InvalidOperationException("this country exists!");
            }
            Country newcountry = new Country()
            {
                Name = createcountry.Name
            };
            _context.Countries.Add(newcountry);
            await _context.SaveChangesAsync();
            return newcountry;
        }

        public async Task UpdateCountry(int id, UpdateCountryDto upcountrydto)
        {
            var result = await _context.Countries.FirstOrDefaultAsync(x => x.Id == id);
            if (result == null)
            {
                throw new KeyNotFoundException("Country not found!!");
            }
            result.Name = upcountrydto.Name;
            await _context.SaveChangesAsync();
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
