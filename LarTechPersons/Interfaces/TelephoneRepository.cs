using LarTechPersons.Context;
using LarTechPersons.Model;
using Microsoft.EntityFrameworkCore;

namespace LarTechPersons.Interfaces;

public class TelephoneRepository : ITelephoneRepository
{
    private readonly ApplicationDbContext _context;

    public TelephoneRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Telephone GetById(Guid id)
    {
        return _context.Telephones.FirstOrDefault(t => t.Id == id);
    }

    public IEnumerable<Telephone> GetAll()
    {
        return _context.Telephones.ToList();
    }

    public void Add(Telephone telephone)
    {
        _context.Telephones.Add(telephone);
        _context.SaveChanges();
    }

    public void Update(Telephone telephone)
    {
        _context.Entry(telephone).State = EntityState.Modified;
        _context.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var telephone = _context.Telephones.Find(id);
        if (telephone != null)
        {
            _context.Telephones.Remove(telephone);
            _context.SaveChanges();
        }
    }
}