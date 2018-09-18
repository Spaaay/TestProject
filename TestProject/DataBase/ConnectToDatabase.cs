using System;
using System.Data.Entity;
using System.Data.SQLite;
using System.Linq;

namespace TestProject.DataBase
{
    public static class ConnectToDataBase
    {
        public static TestProjectContext Db;

        static ConnectToDataBase()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SQLiteConnection sqlite = new SQLiteConnection(connection);
            sqlite.Open();
            Db = new TestProjectContext();
            Db.Disciplines.Load();
            Console.WriteLine(Db.Disciplines.FirstOrDefault());
            Console.ReadLine();
        }
    }
}
