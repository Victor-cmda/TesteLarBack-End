using System.ComponentModel.DataAnnotations.Schema;

namespace LarTechPersons.Model;

public class Telephone : BaseEntity
{
    public string Number { get; set; }
    public int TypeNumber { get; set; }
    
    [ForeignKey("Person")]
    public Guid? PersonId { get; set; }
}