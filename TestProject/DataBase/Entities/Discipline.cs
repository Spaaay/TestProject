using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Aggression")]
    public class Discipline { 

    public int Id { get; set; }

    public string dis_name { get; set; }
}
}
