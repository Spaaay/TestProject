using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SQLite;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Web.Http;
using System.Web.Http.Cors;
using Microsoft.Web.Http;
using Newtonsoft.Json;
using TestProject.DataBase.DataBase;
using TestProject.DataBase.DataBase.Entities;

namespace TestProject.WebApi.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    [ApiVersion("1.0")]
    public class UserController : ApiController
    {
        //GET: api/User
        public string Get()
        {
            try
            {
                var users = new List<User>();
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Users.Load();
                        users = context.Users.ToList();
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

        // GET: api/User/5
        public User Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                var u = new User();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    if (context.Users.Find(id) != null)
                    {
                        u = context.Users.Find(id);
                    }
                }
                return u;
            }
        }

        // POST: api/User
        public void Post([FromBody]User value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    if (value != null)
                    {
                        context.Users.Add(value);
                        context.SaveChanges();
                    }
                }
            }
        }

        // PUT: api/User/5
        public void Put(User value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    var r = context.Users.Find(value.Id);
                    if (r != null)
                    {
                        r.FullName = value.FullName;
                        r.Login = value.Login;
                        r.Password = value.Password;
                        r.GroupId = value.GroupId;
                        context.SaveChanges();
                    }
                }
            }
        }


        // DELETE: api/User/5
        public void Delete(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    var temp = context.Users.Find(id);
                    if (temp != null)
                    {
                        context.Users.Load();
                        context.Users.Remove(temp);
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


