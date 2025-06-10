using Microsoft.AspNetCore.Mvc;
using UserManagement.API.Services; // Para TarefaService
using UserManagement.API.DTOs;    // Para TarefaDto
using UserManagement.API.Models;  // Para StatusTarefa enum

namespace UserManagement.API.Controllers;

[Route("api/[controller]")] // Define a rota base como /api/tarefas
[ApiController]
public class TarefasController : ControllerBase
{
    private readonly TarefaService _tarefaService;
    private readonly ILogger<TarefasController> _logger;

    public TarefasController(TarefaService tarefaService, ILogger<TarefasController> logger)
    {
        _tarefaService = tarefaService;
        _logger = logger;
    }

    // GET: api/tarefas/agendadas
    [HttpGet("agendadas")]
    public async Task<IActionResult> GetTarefasAgendadas([FromQuery] int? setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasAgendadas. SetorId: {SetorId}", setorId);
        try
        {
            var tarefas = await _tarefaService.GetTarefasAgendadasAsync(setorId);
            if (!tarefas.Any())
            {
                return NotFound("Nenhuma tarefa agendada encontrada.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas agendadas.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/do-dia
    [HttpGet("do-dia")]
    public async Task<IActionResult> GetTarefasDoDia([FromQuery] DateTime? dia, [FromQuery] int? setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDoDia. Dia: {Dia}, SetorId: {SetorId}", dia, setorId);
        try
        {
            var tarefas = await _tarefaService.GetTarefasDoDiaAsync(dia, setorId);
            if (!tarefas.Any())
            {
                return NotFound("Nenhuma tarefa encontrada para o dia especificado.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas do dia.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/do-dia/setor/{setorId}
    [HttpGet("do-dia/setor/{setorId:int}")] // :int é uma restrição de rota
    public async Task<IActionResult> GetTarefasDoDiaPorSetor(int setorId, [FromQuery] DateTime? dia)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDoDiaPorSetor. SetorId: {SetorId}, Dia: {Dia}", setorId, dia);
        try
        {
            var tarefas = await _tarefaService.GetTarefasDoDiaPorSetorAsync(setorId, dia);
            if (!tarefas.Any())
            {
                return NotFound($"Nenhuma tarefa encontrada para o setor {setorId} no dia especificado.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas do dia por setor.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/da-semana
    [HttpGet("da-semana")]
    public async Task<IActionResult> GetTarefasDaSemana([FromQuery] DateTime? dataInicioSemana, [FromQuery] int? setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDaSemana. DataIncioSemana: {DataInicioSemana}, SetorId: {SetorId}", dataInicioSemana, setorId);
        try
        {
            var tarefas = await _tarefaService.GetTarefasDaSemanaAsync(dataInicioSemana, setorId);
            if (!tarefas.Any())
            {
                return NotFound("Nenhuma tarefa encontrada para a semana especificada.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas da semana.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/da-semana/setor/{setorId}
    [HttpGet("da-semana/setor/{setorId:int}")]
    public async Task<IActionResult> GetTarefasDaSemanaPorSetor(int setorId, [FromQuery] DateTime? dataInicioSemana)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDaSemanaPorSetor. SetorId: {SetorId}, DataIncioSemana: {DataInicioSemana}", setorId, dataInicioSemana);
        try
        {
            var tarefas = await _tarefaService.GetTarefasDaSemanaPorSetorAsync(setorId, dataInicioSemana);
            if (!tarefas.Any())
            {
                return NotFound($"Nenhuma tarefa encontrada para o setor {setorId} na semana especificada.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas da semana por setor.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/do-mes
    [HttpGet("do-mes")]
    public async Task<IActionResult> GetTarefasDoMes([FromQuery] int ano, [FromQuery] int mes, [FromQuery] int? setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDoMes. Ano: {Ano}, Mes: {Mes}, SetorId: {SetorId}", ano, mes, setorId);
        if (mes < 1 || mes > 12) return BadRequest("Mês inválido. Deve ser entre 1 e 12.");
        if (ano < 2000 || ano > 2100) return BadRequest("Ano inválido."); // Validação básica
        try
        {
            var tarefas = await _tarefaService.GetTarefasDoMesAsync(ano, mes, setorId);
            if (!tarefas.Any())
            {
                return NotFound("Nenhuma tarefa encontrada para o mês e ano especificados.");
            }
            return Ok(tarefas);
        }
        catch (ArgumentOutOfRangeException ex) // Captura a exceção específica do serviço
        {
            _logger.LogWarning(ex, "Argumento inválido na busca de tarefas do mês.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas do mês.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/do-mes/{ano}/{mes}/setor/{setorId}
    [HttpGet("do-mes/{ano:int}/{mes:int}/setor/{setorId:int}")]
    public async Task<IActionResult> GetTarefasDoMesPorSetor(int ano, int mes, int setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasDoMesPorSetor. Ano: {Ano}, Mes: {Mes}, SetorId: {SetorId}", ano, mes, setorId);
        if (mes < 1 || mes > 12) return BadRequest("Mês inválido. Deve ser entre 1 e 12.");
        if (ano < 2000 || ano > 2100) return BadRequest("Ano inválido.");
        try
        {
            var tarefas = await _tarefaService.GetTarefasDoMesPorSetorAsync(ano, mes, setorId);
            if (!tarefas.Any())
            {
                return NotFound($"Nenhuma tarefa encontrada para o setor {setorId} no mês/ano especificado.");
            }
            return Ok(tarefas);
        }
        catch (ArgumentOutOfRangeException ex)
        {
            _logger.LogWarning(ex, "Argumento inválido na busca de tarefas do mês por setor.");
            return BadRequest(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas do mês por setor.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/por-status
    [HttpGet("por-status")]
    public async Task<IActionResult> GetTarefasPorStatus([FromQuery] StatusTarefa status, [FromQuery] int? setorId)
    {
        _logger.LogInformation("Recebida requisição para GetTarefasPorStatus. Status: {Status}, SetorId: {SetorId}", status, setorId);
        try
        {
            var tarefas = await _tarefaService.GetTarefasPorStatusAsync(status, setorId);
            if (!tarefas.Any())
            {
                return NotFound($"Nenhuma tarefa encontrada com o status {status}.");
            }
            return Ok(tarefas);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefas por status.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }

    // GET: api/tarefas/{id}
    [HttpGet("{id:int}", Name = "GetTarefaById")] // Nomear a rota é útil para CreatedAtAction de outros módulos
    public async Task<IActionResult> GetTarefaById(int id)
    {
        _logger.LogInformation("Recebida requisição para GetTarefaById. Id: {Id}", id);
        try
        {
            var tarefa = await _tarefaService.GetTarefaByIdAsync(id);
            if (tarefa == null)
            {
                return NotFound($"Tarefa com ID {id} não encontrada.");
            }
            return Ok(tarefa);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao buscar tarefa por ID.");
            return StatusCode(500, "Ocorreu um erro interno ao processar sua solicitação.");
        }
    }
}