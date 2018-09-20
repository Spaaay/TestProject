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
            var result = string.Empty;
            if (Db.Teachers.Find(id) != null)
            {
                var t = Db.Teachers.Find(id);
                foreach (var w in t.Disciplines)
                {
                    result = result + w.DisciplineName + " ";
                }
                return ("Имя: " +  t.FullName + " Тел: " + t.Phone + " Предметы:  " + result).Trim();
            }
            return "Не найдено учителя";
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
