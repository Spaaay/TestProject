using System.Collections.Generic;
using System.Web.Http;
using Microsoft.Web.Http;

namespace TestProject.WebApi.Controllers
{

    [ApiVersion("1.0")]
    public class TokenController : ApiController
    {
        // GET: api/Token
        [Route("api/token")]
        public IEnumerable<string> Get()
        {



            return new string[] { "value1", "value2" };
        }

        // GET: api/Token/5
        public string Get(int id)
        {

            return "value";
        }

        // POST: api/Token
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Token/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Token/5
        public void Delete(int id)
        {
        }
    }
}
