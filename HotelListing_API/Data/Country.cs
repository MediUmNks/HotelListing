namespace HotelListing_API.Data
{
    public class Country()
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public IList<Hotels> Hotellist { get; set; } = [];
    }
}
