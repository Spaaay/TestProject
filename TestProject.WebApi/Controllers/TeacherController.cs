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
    public class TeacherController : ApiController
    {
        //GET: api/Teacher
        public IEnumerable<Teacher> Get()
        {
            try
            {
                var users = new List<Teacher>();
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Teachers.Load();
                        users = context.Teachers.ToList();
                    }
                }
                return users;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        // GET: api/Teacher/5
        public Teacher Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                var t = new Teacher();
                using (var context = new TestProjectContext())
                {
                    context.Teachers.Load();
                    if (context.Teachers.Find(id) != null)
                    {
                        t = context.Teachers.Find(id);
                    }
                }
                return t;
            }
        }

        // POST: api/Teacher
        public void Post([FromBody]Teacher value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Teachers.Load();
                    context.Teachers.Add(value);
                    context.SaveChanges();
                }
            }
        }

        // PUT: api/Teacher/5
        public void Put(int id, [FromBody]Teacher value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Teachers.Load();
                    var r = context.Teachers.Find(id);
                    if (r != null)
                    {
                        r.FullName = value.FullName;
                        r.Phone = value.Phone;
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE: api/Teacher/5
        public void Delete(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    var temp = context.Teachers.Find(id);
                    if (temp != null)
                    {
                        context.Teachers.Load();
                        context.Teachers.Remove(temp);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}
