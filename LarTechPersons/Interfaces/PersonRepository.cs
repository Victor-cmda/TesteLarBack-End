using LarTechPersons.Context;
using LarTechPersons.Model;
using Microsoft.EntityFrameworkCore;

namespace LarTechPersons.Interfaces;
    public class PersonRepository : IPersonRepository
    {
        private readonly ApplicationDbContext _context;

        public PersonRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Person GetById(Guid id)
        {
            return _context.Persons.FirstOrDefault(p => p.Id == id);
        }

        public IEnumerable<Person> GetAll()
        {
            return _context.Persons.ToList();
        }

        public void Add(Person person)
        {
            _context.Persons.Add(person);
            _context.SaveChanges();
        }

        public void Update(Person person)
        {
            _context.Entry(person).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            var person = _context.Persons.Find(id);
            if (person != null)
            {
                _context.Persons.Remove(person);
                _context.SaveChanges();
            }
        }
    }
