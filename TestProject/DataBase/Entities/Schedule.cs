using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Schedule")]
    class Schedule
    {
        public int Id { get; set; }

        public string DisciplineId { get; set; }

        public string Data { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string TeacherId { get; set; }

        public string GroupId { get; set; }
    }
}
