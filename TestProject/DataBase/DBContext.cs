using System.Data.Entity;

namespace TestProject.DataBase
{
    public class DBContext : DbContext
    {
        public DBContext() : base("DefaultConnection")
            {
        }
       //public DbSet<Agression> Agressions { get; set; }

       

    }
}
