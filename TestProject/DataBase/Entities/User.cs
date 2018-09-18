using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("User")]
    public class User
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string GroupId { get; set; }
    }
}
