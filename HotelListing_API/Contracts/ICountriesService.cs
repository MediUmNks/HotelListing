using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Country;

namespace HotelListing_API.Contracts
{
    public interface ICountriesService
    {
        Task<Country> CreateCountry(CreateCountryDto createcountry);
        Task DeleteCountry(int id);
        Task<IEnumerable<GetCountriesDto>?> GetCountries();
        Task<GetCountryDto> GetCountryById(int id);
        Task UpdateCountry(int id, UpdateCountryDto upcountrydto);
    }
}