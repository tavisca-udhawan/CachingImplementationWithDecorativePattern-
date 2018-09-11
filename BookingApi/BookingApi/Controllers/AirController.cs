using BookingApi.Models;
using BookingApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApi.Controllers
{
    [RoutePrefix("api/flight")]
    public class AirController : ApiController
    {
        
         [Route("add")]
            [HttpPost]
            public IHttpActionResult AddFlight([FromBody]Flight flight)
            {
                var isSuccessful = FlightData.Add(flight);
                if (isSuccessful)
                    return Ok("Flight Added successfully in Database");
                return BadRequest("Could not able to add flight");
            }

            [Route("get")]
            [HttpGet]
            public IHttpActionResult GetAllFlights()
            {
                var flights = FlightData.GetAll();
                if (flights != null)
                {
                    return Ok(flights);
                }
                return NotFound();
            }
            [Route("book/{id:guid}")]
            [HttpPut]
            public IHttpActionResult Book(Guid id)
            {
                var flight = FlightData.Book(id);
                if (flight)
                {
                    return Ok(flight);
                }
                return BadRequest("Could not able to Book flight");
            }
            [Route("save/{id:guid}")]
            [HttpPut]
            public IHttpActionResult Save(Guid id)
            {
                var flight = FlightData.Save(id);
                if (flight)
                {
                    return Ok(flight);
                }
                return BadRequest("Could not Save Flight");
            }
            // GET: api/Air
            public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Air/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Air
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Air/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Air/5
        public void Delete(int id)
        {
        }
    }
}
