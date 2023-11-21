using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public interface IPersonRepository
{
    Task<Person> GetById(Guid id);
    Task<IEnumerable<Person>> GetAll();
    Task Add(Person person);
    Task Update(Person person);
    Task Delete(Guid id);
}