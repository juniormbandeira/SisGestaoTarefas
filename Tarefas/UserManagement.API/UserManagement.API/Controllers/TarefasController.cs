using Microsoft.AspNetCore.Mvc;
using UserManagement.API.DTOs;
using UserManagement.API.Services;

[ApiController]
[Route("api/[controller]")]
public class TarefasController : ControllerBase
{
    private readonly TarefaService _tarefaService;

    public TarefasController(TarefaService tarefaService)
    {
        _tarefaService = tarefaService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] TarefaDto.CreateRequest dto)
    {
        try
        {
            var tarefa = await _tarefaService.CreateTarefa(dto);
            return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro interno ao criar a tarefa.", detalhe = ex.Message });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tarefa = await _tarefaService.GetTarefaById(id);
        return tarefa != null ? Ok(tarefa) : NotFound(new { message = "Tarefa não encontrada." });
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TarefaDto.UpdateRequest dto)
    {
        try
        {
            await _tarefaService.AtualizarTarefaAsync(id, dto);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Tarefa não encontrada para atualização." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao atualizar a tarefa.", detalhe = ex.Message });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Cancelar(int id)
    {
        try
        {
            await _tarefaService.CancelarTarefa(id);
            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { message = "Tarefa não encontrada para exclusão." });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { message = "Erro ao excluir a tarefa.", detalhe = ex.Message });
        }
    }
}