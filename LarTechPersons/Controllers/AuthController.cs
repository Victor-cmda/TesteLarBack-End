using LarTechPersons.Interfaces;
using LarTechPersons.Model;
using LarTechPersons.Services;
using Microsoft.AspNetCore.Mvc;

namespace LarTechPersons.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    
    public AuthController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    [HttpPost]
    public async Task<IActionResult> Auth([FromBody] User user)
    {
        var userDb = await _userRepository.GetByUsername(user.Username);

        if (userDb == null)
        {
            return BadRequest("Usuário ou Senha inválida.");
        }

        bool isPasswordValid = BCrypt.Net.BCrypt.Verify(user.Password, userDb.Password);
        if (!isPasswordValid)
        {
            return BadRequest("Usuário ou Senha inválida.");
        }

        var token = TokenService.GenerateToken(user);
        return Ok(token);
    }
}