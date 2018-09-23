using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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
    public class AuthorizationController : ApiController
    {
        // POST: api/Authorization
        public string Post([FromBody] User value)
        {
            User user = null;
            using (var connection = ConnectToDataBase.GetConnection())
            {
                connection.Open();
                using (SQLiteCommand fmd = connection.CreateCommand())
                {
                    fmd.CommandText = $"Select * from user t where t.login ='{value.Login}' and t.password='{value.Password}'";
                    fmd.CommandType = CommandType.Text;
                    SQLiteDataReader reader = fmd.ExecuteReader();
                    while (reader.Read())
                    {
                        user = new User
                        {
                            GroupId = Convert.ToInt32(reader["group_id"]),
                            FullName = Convert.ToString(reader["fullname"]),
                            Login = Convert.ToString(reader["login"]),
                            Password = Convert.ToString(reader["password"])
                        };
                    }
                    return user != null
                        ? JsonConvert.SerializeObject(user, Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            })
                        : null;
                }
            }
        }
    }
}
