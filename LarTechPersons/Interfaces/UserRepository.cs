using LarTechPersons.Context;
using LarTechPersons.Model;
using Microsoft.EntityFrameworkCore;

namespace LarTechPersons.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;
    
    public UserRepository(ApplicationDbContext context)
    {
        _context = context;    
    }

    public async Task<User> GetByUsername(String username)
    {
        return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }

    public async Task Add(User user)
    {
        user.Password = HashPassword(user.Password);
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
    }
    
    private string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}