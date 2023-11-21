using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public interface IUserRepository
{
    Task<User> GetByUsername(String username);
    Task Add(User user);
}