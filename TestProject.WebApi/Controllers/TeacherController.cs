using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase.Entities;
using static TestProject.DataBase.ConnectToDataBase;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class TeacherController : ApiController
    {
        [Route("api/user")]
        // GET: api/Teacher
        public IEnumerable<Teacher> Get()
        {
            Db.Teachers.Load();
            var r = Db.Teachers;
            return r;
        }

        // GET: api/Teacher/5
        public string Get(int id)
        {
            Db.Teachers.Load();
            var r = Db.Teachers.Find(id)?.FullName;
            return r;
        }

        // POST: api/Teacher
        public void Post([FromBody]Teacher value)
        {
            Db.Teachers.Load();
            Db.Teachers.Add(value);
            Db.SaveChanges();
        }

        // PUT: api/Teacher/5
        public void Put(int id, [FromBody]Teacher value)
        {
            Db.Teachers.Load();
            var r = Db.Teachers.Find(id);
            r.FullName = value.FullName;
            r.Phone = value.Phone;
            Db.SaveChanges();
        }

        // DELETE: api/Teacher/5
        public void Delete(int id)
        {
            var temp = Db.Teachers.Find(id);
            if (temp != null)
            {
                Db.Teachers.Load();
                Db.Teachers.Remove(temp);
                Db.SaveChanges();
            }
        }
    }
}
