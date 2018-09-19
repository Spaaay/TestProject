using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Discipline")]
    public class Discipline { 

    public int id { get; set; }

    public string dis_name { get; set; }
}
}
