using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Discipline")]
    public class Discipline
    {
        [Key]
        public int id { get; set; }
        [Column("dis_name")]
        public string DisciplineName { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
