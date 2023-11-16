namespace LarTechPersons.Model;

public class Person : BaseEntity
{
    public string Name { get; set; }
    public string Cpf { get; set; }
    public DateTime DateBirthday { get; set; }
    public bool Active { get; set; }
    public virtual ICollection<Telephone> Telephones { get; set; }
}
