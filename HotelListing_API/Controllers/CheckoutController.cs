using HotelListing_API.Data;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HotelListing_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutController : ControllerBase
    {
        private static List<Hotels> hotel = new List<Hotels>()
    {
        new Hotels(){Id=1,Tilte="ParsHotel",Rating=3.67},
        new Hotels(){Id=2,Tilte="Spinza",Rating=4},
        new Hotels(){Id=3,Tilte="Darvish",Rating=4.3}
    };
        // GET: api/<Hotel>
        [HttpGet]
        public ActionResult<IEnumerable<Hotels>> Get()
        {
            return Ok(hotel);
        }

        //GET api/<Hotel>/5
        [HttpGet("{id}")]
        public ActionResult Get(int id)
        {
            var result = hotel.FirstOrDefault(h => h.Id == id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST api/<Hotel>
        [HttpPost]
        public ActionResult Post([FromBody] Hotels newhotel)
        {
            var result = hotel.Any(h => h.Id == newhotel.Id || h.Tilte == newhotel.Tilte);
            if (result)
            {
               return BadRequest("This Hotel already exsits");
            }
            hotel.Add(newhotel);
            return CreatedAtAction(nameof(Get), new { newhotel.Id }, newhotel);
        }

        // PUT api/<Hotel>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Hotels EditedHotel)
        {
            var existhotel = hotel.FirstOrDefault(h => h.Id == id);
            if (existhotel == null)
            {
                return NotFound();
            }

            existhotel.Tilte = EditedHotel.Tilte;
            existhotel.Rating = EditedHotel.Rating;
            return NoContent();
        }

        // DELETE api/<Hotel>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var deletehotel = hotel.FirstOrDefault(h => h.Id == id);
            if (deletehotel == null)
            {
                return NotFound();
            }
            hotel.Remove(deletehotel);
            return NoContent();
        }
    }
}
