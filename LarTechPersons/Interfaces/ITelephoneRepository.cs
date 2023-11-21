using LarTechPersons.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LarTechPersons.Interfaces
{
    public interface ITelephoneRepository
    {
        Task<Telephone> GetById(Guid id);
        Task<IEnumerable<Telephone>> GetAll();
        Task Add(Telephone telephone);
        Task Update(Telephone telephone);
        Task Delete(Guid id);
    }
}