using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Web.Http;
using Newtonsoft.Json;
using TestProject.DataBase.DataBase;
using TestProject.DataBase.DataBase.Entities;

namespace TestProject.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders:"*")]
    [ApiVersion("1.0")]
    public class TeacherController : ApiController
    {
        //GET: api/Teacher
        public string  Get()
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
                        return JsonConvert.SerializeObject(users, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            });
                    }
                }
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
                    if (value != null)
                    {
                        context.Teachers.Add(value);
                        context.SaveChanges();
                    }
                }
            }
        }

        // PUT: api/Teacher/5
        public void Put(Teacher value)
        {
            if (value != null && value.Id != 0)
            {
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Teachers.Load();
                        var r = context.Teachers.Find(value.Id);
                        if (r != null)
                        {
                            r.FullName = value.FullName;
                            r.Phone = value.Phone;
                            context.SaveChanges();
                        }
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

        [HttpOptions]
        public HttpResponseMessage Options()
        {
            return new HttpResponseMessage { StatusCode = HttpStatusCode.OK };
        }
    }
}
