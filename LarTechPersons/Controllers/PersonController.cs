using Microsoft.AspNetCore.Mvc;
using LarTechPersons.Model;
using LarTechPersons.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LarTechPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;
        private readonly ITelephoneRepository _telephoneRepository;

        public PersonController(IPersonRepository personRepository, ITelephoneRepository telephoneRepository)
        {
            _personRepository = personRepository;
            _telephoneRepository = telephoneRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var persons = await _personRepository.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var person = await _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Person person)
        {
            if (ModelState.IsValid)
            {
                var personToAdd = new Person()
                {
                    Name = person.Name,
                    Active = person.Active,
                    Cpf = person.Cpf,
                    DateBirthday = person.DateBirthday,
                    CreatedAt = person.CreatedAt,
                    UpdatedAt = person.UpdatedAt,
                };
                await _personRepository.Add(personToAdd);
                
                if (person.Telephones != null)
                {
                    foreach (var telephone in person.Telephones)
                    {
                        var existingTelephone = await _telephoneRepository.GetById(telephone.Id);
                        if (existingTelephone != null)
                        {
                            existingTelephone.PersonId = personToAdd.Id;
                            await _telephoneRepository.Update(existingTelephone);
                        }
                    }
                }

                return CreatedAtAction("GetById", new { id = person.Id }, person);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingPerson = await _personRepository.GetById(person.Id);

                if (existingPerson == null)
                {
                    return NotFound();
                }

                foreach (var existingTelephone in existingPerson.Telephones.ToList())
                {
                    existingTelephone.PersonId = null;
                    _telephoneRepository.Update(existingTelephone);
                }

                foreach (var telephone in person.Telephones)
                {
                    var existingTelephone = await _telephoneRepository.GetById(telephone.Id);
                    if (existingTelephone != null)
                    {
                        existingTelephone.Number = telephone.Number;
                        existingTelephone.TypeNumber = telephone.TypeNumber;
                        existingTelephone.PersonId = person.Id;
                        await _telephoneRepository.Update(existingTelephone);
                    }
                    else
                    {
                        telephone.PersonId = person.Id;
                        await _telephoneRepository.Add(telephone);
                    }
                }

                existingPerson.Name = person.Name;
                existingPerson.Cpf = person.Cpf;
                existingPerson.DateBirthday = person.DateBirthday;
                existingPerson.Active = person.Active;

                await _personRepository.Update(existingPerson);
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var person = await _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personRepository.Delete(id);
            return NoContent();
        }
    }
}