using System;
using System.Data.SQLite;

namespace TestProject.DataBase.DataBase
{
    public class ConnectToDataBase
    {
        public TestProjectContext Db;

        public static SQLiteConnection GetConnection()
        {
            try
            {
                var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                Console.WriteLine(connection);
                return new SQLiteConnection(connection, true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
