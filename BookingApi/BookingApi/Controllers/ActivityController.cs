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
    [RoutePrefix("api/activity")]

    public class ActivityController : ApiController
    {  [Route("add")]
            [HttpPost]
            public IHttpActionResult AddActivity([FromBody]Activity activity)
            {
                var isSuccessful = ActivityData.Add(activity);
                if (isSuccessful)
                    return Ok("Activity Added successfully in Database");
                return BadRequest("Could not able to add activity");
            }

            [Route("get")]
            [HttpGet]
            public IHttpActionResult GetAllActivities()
            {
                var activities = ActivityData.GetAll();
                if (activities != null)
                {
                    return Ok(activities);
                }
                return NotFound();
            }
            [Route("book/{id:guid}")]
            [HttpPut]
            public IHttpActionResult Book(Guid id)
            {
                var activity = ActivityData.Book(id);
                if (activity)
                {
                    return Ok(activity);
                }
                return BadRequest("Could not able to Book activity");
            }
            [Route("save/{id:guid}")]
            [HttpPut]
            public IHttpActionResult Save(Guid id)
            {
                var activity = ActivityData.Save(id);
                if (activity)
                {
                    return Ok(activity);
                }
                return BadRequest("Could not Save Activity");
            }
            // GET: api/Activity
            public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Activity/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Activity
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Activity/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Activity/5
        public void Delete(int id)
        {
        }
    }
}
