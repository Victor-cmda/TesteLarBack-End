using Microsoft.AspNetCore.Mvc;
using LarTechPersons.Model;
using System;
using System.Collections.Generic;
using LarTechPersons.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace LarTechPersons.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class TelephoneController : ControllerBase
    {
        private readonly ITelephoneRepository _telephoneRepository;

        public TelephoneController(ITelephoneRepository telephoneRepository)
        {
            _telephoneRepository = telephoneRepository;
        }

        [HttpGet]
        public IActionResult Get([FromQuery] string filter = "")
        {
            if (filter == "NoUser")
            {
                var telephonesWithoutUser = _telephoneRepository
                    .GetAll()
                    .Where(t => t.PersonId == null);
                return Ok(telephonesWithoutUser);
            }
            else
            {
                var telephones = _telephoneRepository.GetAll();
                return Ok(telephones);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var telephone = _telephoneRepository.GetById(id);
            if (telephone == null)
            {
                return NotFound();
            }
            return Ok(telephone);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Telephone telephone)
        {
            if (ModelState.IsValid)
            {
                _telephoneRepository.Add(telephone);
                return CreatedAtAction("GetById", new { id = telephone.Id }, telephone);
            }
            return BadRequest(ModelState);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] Telephone telephone)
        {
            if (id != telephone.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _telephoneRepository.Update(telephone);
                return NoContent();
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var telephone = _telephoneRepository.GetById(id);
            if (telephone == null)
            {
                return NotFound();
            }

            _telephoneRepository.Delete(id);
            return NoContent();
        }
    }
}