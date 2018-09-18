using System.Data.Entity;
using System.Data.SQLite;

namespace TestProject.DataBase
{
    public static class ConnectToDataBase
    {
        public static DBContext Db;

        static ConnectToDataBase()
        {
            var connection = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SQLiteConnection sqlite = new SQLiteConnection(connection);
            sqlite.Open();
            Db = new DBContext();
        }
    }
}
