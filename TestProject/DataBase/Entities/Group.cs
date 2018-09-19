using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Group")]
    public class Group
    {
        public int Id { get; set; }

        [Column("group_name")]
        public string GroupName { get; set; }

        [Column("start_date")]
        public string StartDate { get; set; }

        [Column("end_date")]
        public string EndDate { get; set; }

    }
}
