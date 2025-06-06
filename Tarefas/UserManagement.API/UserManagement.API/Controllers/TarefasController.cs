using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Services;
using UserManagement.API.DTOs;

namespace UserManagement.API.Controllers;

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
        var tarefa = await _tarefaService.CreateTarefa(dto);
        return CreatedAtAction(nameof(GetById), new { id = tarefa.Id }, tarefa);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var tarefa = await _tarefaService.GetTarefaById(id);
        return tarefa != null ? Ok(tarefa) : NotFound();
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(int id, [FromBody] TarefaDto.UpdateRequest dto)
    {
        await _tarefaService.UpdateTarefa(id, dto);
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Cancelar(int id)
    {
        await _tarefaService.CancelarTarefa(id);
        return NoContent();
    }
}
