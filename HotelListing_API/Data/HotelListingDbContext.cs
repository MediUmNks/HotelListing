using HotelListing_API.Data.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;

namespace HotelListing_API.Data
{
    public class HotelListingDbContext : DbContext
    {
        public HotelListingDbContext(DbContextOptions<HotelListingDbContext> options) : base(options)
        {

        }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Hotels> Hotel { get; set; }
    }
}
