using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using Microsoft.Web.Http;
using TestProject.DataBase.Entities;
using static TestProject.DataBase.ConnectToDataBase;

namespace TestProject.WebApi.Controllers
{
    [ApiVersion("1.0")]
    public class GroupController : ApiController
    {
        // GET: api/Group
        [Route("api/discipline")]
        public IEnumerable<Group> Get()
        {
            Db.Groups.Load();
            var r = Db.Groups;
            return r;
        }

        // GET: api/Group/5
        public string Get(int id)
        {
            Db.Groups.Load();
            var r = Db.Groups.Find(id)?.GroupName;
            return r;
        }

        // POST: api/Group
        public void Post([FromBody]Group value)
        {
            Db.Groups.Load();
            Db.Groups.Add(value);
            Db.SaveChanges();
        }

        // PUT: api/Group/5
        public void Put(int id, [FromBody]Group value)
        {
            Db.Groups.Load();
            var r = Db.Groups.Find(id);
            r.GroupName = value.GroupName;
            r.EndDate = value.EndDate;
            r.StartDate = value.StartDate;
            Db.SaveChanges();
        }

        // DELETE: api/Group/5
        public void Delete(int id)
        {
            var temp = Db.Groups.Find(id);
            if (temp != null)
            {
                Db.Groups.Load();
                Db.Groups.Remove(temp);
                Db.SaveChanges();
            }
        }
    }
}
