using LarTechPersons.Interfaces;
using LarTechPersons.Model;
using LarTechPersons.Services;
using Microsoft.AspNetCore.Mvc;

namespace LarTechPersons.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : Controller
{
    private readonly IUserRepository _userRepository;
    
    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost]
    public IActionResult AddUser([FromBody] User user)
    {
        var userDb = _userRepository.GetByUsername(user.Username);

        if (userDb != null)
        {
            return BadRequest("Usuário já existente");
        }
        
        _userRepository.Add(new User()
        {
            Username = user.Username,
            Password = user.Password
        });        

        return Ok("Usuário Cadastrado com Sucesso!");
    }
}