using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Group")]
    public class Group
    {
        public int Id { get; set; }

        public string GroupName { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }

    }
}
