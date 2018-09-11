using BookingApi.Models;
using BookingApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApi.Controllers
{  [RoutePrefix("api/hotel")]
    public class HotelsController : ApiController
    {
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddHotel([FromBody]Hotel hotel)
        {
            var isSuccessful = HotelData.Add(hotel);
            if (isSuccessful)
                return Ok("Hotel Added successfully in database");
            return BadRequest("Could not able to add hotel");
        }

        [Route("get/{id:guid}")]
        [HttpGet]
        public IHttpActionResult GetHotel(Guid id)
        {
            var hotel = HotelData.Get(id);
            if (hotel != null)
            {
                return Ok(hotel);
            }
            return NotFound();
        }

        [Route("get")]
        [HttpGet]
        public IHttpActionResult GetAllHotels()
        {
            var hotels = HotelData.GetAll();
            if (hotels != null)
            {
                return Ok(hotels);
            }
            return NotFound();
        }
        [Route("book/{id:guid}")]
        [HttpPut]
        public IHttpActionResult Book(Guid id)
        {
            var hotel = HotelData.Book(id);
            if (hotel)
            {
                return Ok(hotel);
            }
            return BadRequest("Could not able to Book hotel");
        }
        [Route("save/{id:guid}")]
        [HttpPut]
        public IHttpActionResult Save(Guid id)
        {
            var hotel = HotelData.Save(id);
            if (hotel)
            {
                return Ok(hotel);
            }
            return BadRequest("Could not Save hotel");
        }

        // GET api/values

        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
