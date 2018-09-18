using System.ComponentModel.DataAnnotations.Schema;

namespace TestProject.DataBase.Entities
{
    [Table("Aggression")]
    class Discipline { 

    public int Id { get; set; }

    public string DisName { get; set; }
}
}
