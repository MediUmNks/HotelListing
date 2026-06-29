using System.ComponentModel.DataAnnotations;

namespace HotelListing_API.DTOs.Country
{
    public class CreateCountryDto
    {
        [Required]
        public string? Name { get; set; }
    }
}
