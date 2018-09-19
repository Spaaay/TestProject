using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Discipline")]
    public class Discipline
    {

        public int id { get; set; }
        [Column("dis_name")]
        public string DisciplineName { get; set; }
    }
}
