using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase;
using TestProject.DataBase.Entities;
using static TestProject.DataBase.ConnectToDataBase;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class DisciplineController : ApiController
    {
        // GET: api/Discipline
        [Route("api/discipline")]
        public IEnumerable<Discipline> Get()
        {
            Db.Disciplines.Load();
            var r = Db.Disciplines;
            return r;
        }

        // GET: api/Discipline/5
        public string Get(int id)
        {
            Db.Disciplines.Load();
            var r = Db.Disciplines.Find(id).DisciplineName;
            return r;
        }

        // POST: api/Discipline
        public void Post([FromBody]Discipline value)
        {
            Db.Disciplines.Load();
            Db.Disciplines.Add(value);
            Db.SaveChanges();

        }

        // PUT: api/Discipline/5
        public void Put(int id, [FromBody]Discipline value)
        {
            Db.Disciplines.Load();
            var r = Db.Disciplines.Find(id);
            r.id = value.id;
            r.DisciplineName = value.DisciplineName;
            Db.SaveChanges();
        }

        // DELETE: api/Discipline/5
        public void Delete(int id)
        {
            var temp = Db.Disciplines.Find(id);
            if (temp != null) { 
            Db.Disciplines.Load();
            Db.Disciplines.Remove(temp);
            Db.SaveChanges();
            }

        }
    }
}
