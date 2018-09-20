using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Teacher")]
    public class Teacher
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        public virtual ICollection<Discipline> Disciplines { get; set; }
    }
}
