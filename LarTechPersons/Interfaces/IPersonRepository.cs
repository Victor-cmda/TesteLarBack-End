using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public interface IPersonRepository
{
    Person GetById(Guid id);
    IEnumerable<Person> GetAll();
    void Add(Person person);
    void Update(Person person);
    void Delete(Guid id);
}