using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public interface ITelephoneRepository
{
    Telephone GetById(Guid id);
    IEnumerable<Telephone> GetAll();
    void Add(Telephone telephone);
    void Update(Telephone telephone);
    void Delete(Guid id);
}