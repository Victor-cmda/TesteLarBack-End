using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public interface IUserRepository
{
    User GetByUsername(String username);
    void Add(User user);
}