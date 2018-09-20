using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Schedule")]
    public class Schedule
    {
        public int Id { get; set; }

        [Column("discipline_id")]
        public int DisciplineId { get; set; }

        [Column("date")]
        public string Data { get; set; }

        [Column("starttime")]
        public string StartTime { get; set; }

        [Column("endtime")]
        public string EndTime { get; set; }

        [Column("teacher_id")]
        public int TeacherId { get; set; }

        [Column("group_id")]
        public int GroupId { get; set; }

        [ForeignKey("TeacherId")]
        public virtual Teacher Teacher { get; set; }

        [ForeignKey("GroupId")]
        public virtual Group Group { get; set; }

        [ForeignKey("DisciplineId")]
        public virtual Discipline Discipline { get; set; }

    }
}
