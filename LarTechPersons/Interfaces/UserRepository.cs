using LarTechPersons.Context;
using LarTechPersons.Model;

namespace LarTechPersons.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;    
    }

    public User GetByUsername(String username)
    {
        return _context.Users.FirstOrDefault(x => x.Username == username);
    }

    public void Add(User user)
    {
        user.Password = HashPassword(user.Password);
        _context.Users.Add(user);
        _context.SaveChanges();
    }
    
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}