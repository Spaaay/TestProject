using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Teacher")]
    class Teacher
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Phone { get; set; }
    }
}
