using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Services;

namespace UserManagement.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PerfisController : ControllerBase
{
    private readonly PerfilService _perfilService;

    public PerfisController(PerfilService perfilService)
    {
        _perfilService = perfilService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] PerfilDto.CreateRequest dto) // Nome do método alterado para Create para evitar conflito
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var perfil = await _perfilService.CreatePerfil(dto);
            return CreatedAtAction(nameof(GetById), new { id = perfil.Id }, perfil);
        }
        catch (KeyNotFoundException ex) // Exceção específica para usuário não encontrado
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var perfil = await _perfilService.GetPerfilById(id);
        return perfil != null ? Ok(perfil) : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] PerfilDto.UpdateRequest dto)
    {
        if (id != dto.Id)
        {
            return BadRequest("ID da rota e ID do corpo da requisição não coincidem.");
        }
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var perfil = await _perfilService.UpdatePerfil(id, dto);
            return Ok(perfil);
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            await _perfilService.DeletePerfil(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (InvalidOperationException ex) // Para o caso de perfil com usuários
        {
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); // Outros erros genéricos
        }
    }
}