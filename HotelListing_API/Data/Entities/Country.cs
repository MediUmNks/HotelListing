namespace HotelListing_API.Data.Entities
{
    public class Country()
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public IList<Hotels> Hotellist { get; set; } = [];
    }
}
