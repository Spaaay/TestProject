using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase.Entities;
using static TestProject.DataBase.ConnectToDataBase;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class UserController : ApiController
    {
        [Route("api/user")]
        // GET: api/User
        public IEnumerable<User> Get()
        {
            Db.Users.Load();
            var r = Db.Users;
            return r;
        }

        // GET: api/User/5
        public string Get(int id)
        {
            Db.Users.Load();
            var r = Db.Users.Find(id)?.FullName;
            return r;
        }

        // POST: api/User
        public void Post([FromBody]User value)
        {
            Db.Users.Load();
            Db.Users.Add(value);
            Db.SaveChanges();
        }

        // PUT: api/User/5
        public void Put(int id, [FromBody]User value)
        {
            Db.Users.Load();
            var r = Db.Users.Find(id);
            r.FullName = value.FullName;
            r.Login = value.Login;
            r.Password = value.Password;
            r.GroupId = value.GroupId;
            Db.SaveChanges();
        }

        // DELETE: api/User/5
        public void Delete(int id)
        {
            var temp = Db.Users.Find(id);
            if (temp != null)
            {
                Db.Users.Load();
                Db.Users.Remove(temp);
                Db.SaveChanges();
            }
        }
    }
}
