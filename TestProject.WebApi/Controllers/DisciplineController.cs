using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase.DataBase;
using TestProject.DataBase.DataBase.Entities;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class DisciplineController : ApiController
    {
        //GET: api/Discipline
        public IEnumerable<Discipline> Get()
        {
            try
            {
                var disciplines = new List<Discipline>();
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Disciplines.Load();
                        disciplines = context.Disciplines.ToList();
                    }
                }
                return disciplines;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        // GET: api/Discipline/5
        public Discipline Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                var d = new Discipline();
                using (var context = new TestProjectContext())
                {
                    context.Disciplines.Load();
                    if (context.Disciplines.Find(id) != null)
                    {
                        d = context.Disciplines.Find(id);
                    }
                }
                return d;
            }
        }

        // POST: api/Discipline
        public void Post([FromBody]Discipline value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Disciplines.Load();
                    context.Disciplines.Add(value);
                    context.SaveChanges();
                }
            }
        }

        // PUT: api/Discipline/5
        public void Put(int id, [FromBody]Discipline value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Disciplines.Load();
                    var r = context.Disciplines.Find(id);
                    if (r != null)
                    {
                        r.DisciplineName = value.DisciplineName;
                        r.TeacherId = value.TeacherId;
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE: api/Discipline/5
        public void Delete(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    var temp = context.Disciplines.Find(id);
                    if (temp != null)
                    {
                        context.Disciplines.Load();
                        context.Disciplines.Remove(temp);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
