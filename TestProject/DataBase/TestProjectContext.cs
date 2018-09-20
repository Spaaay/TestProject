using System.Data.Entity;
using TestProject.DataBase.Entities;

namespace TestProject.DataBase
{
    public class TestProjectContext : System.Data.Entity.DbContext
    {

        public TestProjectContext() : base("DefaultConnection") { }

        public DbSet<Discipline> Disciplines { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Discipline>()
                .HasRequired(t => t.Teacher)
                .WithMany(d => d.Disciplines);
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                  .HasRequired(r => r.Group)
                  .WithMany(u => u.Users);
            base.OnModelCreating(modelBuilder);
        }
    }
}
