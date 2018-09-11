using BookingApi.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BookingApi.Controllers
{
    public class AuthenticateController : ApiController
    {
        [Route("api/authenticate")]
        [HttpPost()]
        public IHttpActionResult Authenticate([FromBody] AuthenticateBinding credentials)
        {
            if (credentials != null)
            {
                AuthenticateProvider provider = new AuthenticateProvider(credentials.UserName, credentials.Password);
                var userType = provider.Authenticate();
                if (userType.HasValue)
                {
                    User currentUser = new User();
                    currentUser.Id = Guid.NewGuid();
                    currentUser.IsAuthenticated = true;
                    currentUser.UserType = userType;

                    return Ok(currentUser);
                }
                return BadRequest("Invalid credentials");
            }
            return BadRequest("Please enter credentials");
        }


        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Authenticate/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Authenticate
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Authenticate/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Authenticate/5
        public void Delete(int id)
        {
        }
    }
}
