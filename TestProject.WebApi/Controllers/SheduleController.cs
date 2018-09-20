using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase.Entities;
using static TestProject.DataBase.ConnectToDataBase;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class SheduleController : ApiController
    {
        // GET: api/Shedule
        [Route("api/discipline")]
        public IEnumerable<Schedule> Get()
        {
            Db.Schedules.Load();
            var r = Db.Schedules;
            return r;
        }

        // GET: api/Shedule/5
        public string Get(int id)
        {
            Db.Schedules.Load();
            if (Db.Schedules.Find(id) != null)
            {
                var r = Db.Schedules.Find(id);
                return ("Дата: " + r.Data + " Начало: " + r.StartTime + " Конец: " + r.EndTime + " Предмет: " + r.Discipline.DisciplineName + " Преподаватель: " + r.Teacher.FullName + " Группа " + r.Group.GroupName).Trim();
            }
            return "Не найдено группы";
        }

        // POST: api/Shedule
        public void Post([FromBody]Schedule value)
        {
            Db.Schedules.Load();
            Db.Schedules.Add(value);
            Db.SaveChanges();
        }

        // PUT: api/Shedule/5
        public void Put(int id, [FromBody]Schedule value)
        {
            Db.Schedules.Load();
            var r = Db.Schedules.Find(id);
            r.Data = value.Data;
            r.DisciplineId = value.DisciplineId;
            r.TeacherId = value.TeacherId;
            r.GroupId = value.GroupId;
            r.StartTime = value.StartTime;
            r.EndTime = value.EndTime;
            Db.SaveChanges();
        }

        // DELETE: api/Shedule/5
        public void Delete(int id)
        {
            var temp = Db.Schedules.Find(id);
            if (temp != null)
            {
                Db.Schedules.Load();
                Db.Schedules.Remove(temp);
                Db.SaveChanges();
            }
        }
    }
}
