using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("TEACHER_DISCIPLINE")]
    public class TeacherDisciple
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("id_teacher")]
        public int TeacherId { get; set; }

        [Column("id_discipline")]
        public int DisciplineId { get; set; }
    }
}
