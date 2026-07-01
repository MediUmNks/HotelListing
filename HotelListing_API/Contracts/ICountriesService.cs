using HotelListing_API.Data.Entities;
using HotelListing_API.DTOs.Country;

namespace HotelListing_API.Contracts
{
    public interface ICountriesService
    {
        Task DeleteCountry(int id);
        Task<IEnumerable<Country>?> GetCountries();
        Task<GetCountryDto> GetCountrغById(int id);
        Task PutCountry(int id, UpdateCountryDto upcountrydto);
    }
}