namespace HotelListing_API.DTOs.Hotel
{
    public record GetHotelDto(int id, string title, double rating, int countryid);
}
public record GetHotelsDto(int id, string title, double rating, int countryid);

