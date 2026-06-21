using System.ComponentModel.DataAnnotations.Schema;

namespace HotelListing_API.Data
{
    public class Hotels
    {
        public int Id { get; set; }
        public string Tilte { get; set; }
        public double Rating { get; set; }
        public int CountryId { get; set; }
        public Country? country { get; set; }
    }
}
