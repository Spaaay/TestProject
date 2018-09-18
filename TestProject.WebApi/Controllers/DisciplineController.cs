using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase;
using TestProject.DataBase.Entities;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class DisciplineController : ApiController
    {
        // GET: api/Discipline
        [Route("api/discipline")]
        public IEnumerable<Discipline> Get()
        {
            ConnectToDataBase.Db.Users.Load();
            //var r = ConnectToDataBase.Db.Disciplines;
            return new List<Discipline>();
        }

        // GET: api/Discipline/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Discipline
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Discipline/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Discipline/5
        public void Delete(int id)
        {
        }
    }
}
