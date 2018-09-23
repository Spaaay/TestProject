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

    [EnableCors(origins: "*", headers: "*", methods: "*", exposedHeaders: "*")]
    [ApiVersion("1.0")]
    public class SheduleController : ApiController
    {
        //GET: api/Shedule
        public string Get()
        {
            try
            {
                var scheldules = new List<Schedule>();
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Schedules.Load();
                        scheldules = context.Schedules.ToList();
                        return JsonConvert.SerializeObject(scheldules, Formatting.None,
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

        // GET: api/Shedule/5
        public Schedule Get(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                var t = new Schedule();
                using (var context = new TestProjectContext())
                {
                    context.Schedules.Load();
                    if (context.Schedules.Find(id) != null)
                    {
                        t = context.Schedules.Find(id);
                    }
                }
                return t;
            }
        }

        // POST: api/Shedule
        public void Post([FromBody]Schedule value)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    context.Schedules.Load();
                    if (value != null)
                    {
                        context.Schedules.Add(value);
                        context.SaveChanges();
                    }
                }
            }
        }

        // PUT: api/Shedule/5
        public void Put(Schedule value)
        {
            if (value != null && value.Id != 0)
            {
                using (var connection = ConnectToDataBase.GetConnection())
                {
                    connection.Open();
                    using (var context = new TestProjectContext())
                    {
                        context.Schedules.Load();
                        var r = context.Schedules.Find(value.Id);
                        if (r != null)
                        {
                            r.TeacherId = value.TeacherId;
                            r.Data = value.Data;
                            r.DisciplineId = value.DisciplineId;
                            r.EndTime = value.EndTime;
                            r.StartTime = value.StartTime;
                            r.GroupId = value.GroupId;
                            context.SaveChanges();
                        }
                    }
                }
            }
        }

        // DELETE: api/Shedule/5
        public void Delete(int id)
        {
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (var context = new TestProjectContext())
                {
                    var temp = context.Schedules.Find(id);
                    if (temp != null)
                    {
                        context.Schedules.Load();
                        context.Schedules.Remove(temp);
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
