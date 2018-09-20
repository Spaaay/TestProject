using System;
using System.Data.SQLite;

namespace TestProject.DataBase
{
    public static class ConnectToDataBase
    {
        public static TestProjectContext Db;

        static ConnectToDataBase()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            Console.WriteLine(connection);
            SQLiteConnection sqlite = new SQLiteConnection(connection, true);
            sqlite.Open();
            Db = new TestProjectContext();
        }
    }
}
