using Microsoft.AspNetCore.Mvc;
using LarTechPersons.Model;
using System;
using System.Collections.Generic;
using LarTechPersons.Interfaces;

namespace LarTechPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
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
                _personRepository.Add(person);
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
                _personRepository.Update(person);
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