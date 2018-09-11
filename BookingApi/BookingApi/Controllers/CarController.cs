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
    [RoutePrefix("api/car")]
    public class CarController : ApiController
    {
        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddCar([FromBody]Car car)
        {
            var isSuccessful = CarData.Add(car);
            if (isSuccessful)
                return Ok("Car Added successfully in Database");
            return BadRequest("Could not able to add car");
        }

     [Route("get")]
        [HttpGet]
        public IHttpActionResult GetAllCars()
        {
            var cars = CarData.GetAll();
            if (cars != null)
            {
                return Ok(cars);
            }
            return NotFound();
        }
        [Route("book/{id:guid}")]
        [HttpPut]
        public IHttpActionResult Book(Guid id)
        {
            var car = CarData.Book(id);
            if (car)
            {
                return Ok(car);
            }
            return BadRequest("Could not able to Book car");
        }
        [Route("save/{id:guid}")]
        [HttpPut]
        public IHttpActionResult Save(Guid id)
        {
            var car = CarData.Save(id);
            if (car)
            {
                return Ok(car);
            }
            return BadRequest("Could not Save car");
        }
        // GET: api/Car
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Car/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Car
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Car/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Car/5
        public void Delete(int id)
        {
        }
    }
}
