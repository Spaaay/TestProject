using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.DataBase.Entities
{
    [Table("User")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("fullname")]
        public string FullName { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("group_id")]
        public int? GroupId { get; set; }

        public virtual Group Group { get; set; }
    }
}
