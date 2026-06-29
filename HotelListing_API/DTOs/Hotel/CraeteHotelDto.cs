namespace HotelListing_API.DTOs.Hotel
{
    public class CreateHotelDto
    {
        public string? Title { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
    }
}
