using LarTechPersons.Context;
using LarTechPersons.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LarTechPersons.Interfaces
{
    public class TelephoneRepository : ITelephoneRepository
    {
        private readonly ApplicationDbContext _context;

        public TelephoneRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Telephone> GetById(Guid id)
        {
            return await _context.Telephones.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Telephone>> GetAll()
        {
            return await _context.Telephones.ToListAsync();
        }

        public async Task Add(Telephone telephone)
        {
            await _context.Telephones.AddAsync(telephone);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Telephone telephone)
        {
            _context.Entry(telephone).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            var telephone = await _context.Telephones.FindAsync(id);
            if (telephone != null)
            {
                _context.Telephones.Remove(telephone);
                await _context.SaveChangesAsync();
            }
        }
    }
}