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
        public IActionResult Get()
        {
            var persons = _personRepository.GetAll();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
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
                _personRepository.Add(personToAdd);
                
                if (person.Telephones != null)
                {
                    foreach (var telephone in person.Telephones)
                    {
                        var existingTelephone = _telephoneRepository.GetById(telephone.Id);
                        if (existingTelephone != null)
                        {
                            existingTelephone.PersonId = personToAdd.Id;
                            _telephoneRepository.Update(existingTelephone);
                        }
                    }
                }

                return CreatedAtAction("GetById", new { id = person.Id }, person);
            }

            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Person person)
        {
            if (id != person.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var existingPerson = _personRepository.GetById(person.Id);

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
                    var existingTelephone = _telephoneRepository.GetById(telephone.Id);
                    if (existingTelephone != null)
                    {
                        existingTelephone.Number = telephone.Number;
                        existingTelephone.TypeNumber = telephone.TypeNumber;
                        existingTelephone.PersonId = person.Id;
                        _telephoneRepository.Update(existingTelephone);
                    }
                    else
                    {
                        telephone.PersonId = person.Id;
                        _telephoneRepository.Add(telephone);
                    }
                }

                existingPerson.Name = person.Name;
                existingPerson.Cpf = person.Cpf;
                existingPerson.DateBirthday = person.DateBirthday;
                existingPerson.Active = person.Active;

                _personRepository.Update(existingPerson);
                return NoContent();
            }

            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var person = _personRepository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            _personRepository.Delete(id);
            return NoContent();
        }
    }
}