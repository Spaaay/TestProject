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
    public class GroupController : ApiController
    {
        //GET: api/Group
        public IEnumerable<Group> Get()
        {
            try
            {
                var groups = new List<Group>();
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Groups.Load();
                        groups = context.Groups.ToList();
                    }
                }
                return groups;
            }
            catch (Exception e)
            {
                Debug.WriteLine(e);
                throw;
            }
        }

        // GET: api/Group/5
        public Group Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                var g = new Group();
                using (var context = new TestProjectContext())
                {
                    context.Groups.Load();
                    if (context.Groups.Find(id) != null)
                    {
                        g = context.Groups.Find(id);
                    }
                }
                return g;
            }
        }

        // POST: api/Group
        public void Post([FromBody]Group value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Groups.Load();
                    context.Groups.Add(value);
                    context.SaveChanges();
                }
            }
        }

        // PUT: api/Group/5
        public void Put(int id, [FromBody]Group value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Groups.Load();
                    var r = context.Groups.Find(id);
                    if (r != null)
                    {
                        r.GroupName = value.GroupName;
                        r.StartDate = value.StartDate;
                        r.EndDate = value.EndDate;
                        context.SaveChanges();
                    }
                }
            }
        }

        // DELETE: api/Group/5
        public void Delete(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    var temp = context.Groups.Find(id);
                    if (temp != null)
                    {
                        context.Groups.Load();
                        context.Groups.Remove(temp);
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}

