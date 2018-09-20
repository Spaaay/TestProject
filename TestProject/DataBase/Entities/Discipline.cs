using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Discipline")]
    public class Discipline
    {
        [Key]
        [Column("id")]
        public int DisciplineId { get; set; }

        [Column("dis_name")]
        public string DisciplineName { get; set; }

        [Column("teacher_id")]
        public int TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }
    }
}
