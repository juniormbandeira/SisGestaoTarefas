using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Services;
using UserManagement.API.DTOs;

namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;

    public UsersController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] UserDto.CreateRequest dto)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = await _userService.CreateUser(dto);
            // O CreatedAtAction espera o nome da action de Get, o objeto de rota e o objeto criado.
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }
        catch (Exception ex)
        {
         
            return BadRequest(ex.Message); 
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetUser(int id) 
    {
        var user = await _userService.GetUserById(id);
        return user != null ? Ok(user) : NotFound();
    }
}