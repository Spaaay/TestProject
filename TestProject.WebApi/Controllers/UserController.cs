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
    public class UserController : ApiController
    {
        //GET: api/User
        public IEnumerable<User> Get()
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

        // GET: api/User/5
        public string Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    var r = context.Users.Find(id)?.FullName;
                    return r;
                }
            }
        }

        // POST: api/User
        public void Post([FromBody] User value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    context.Users.Add(value);
                    context.SaveChanges();
                }
            }
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody] User value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Users.Load();
                    var r = context.Users.Find(id);
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
    }
}


