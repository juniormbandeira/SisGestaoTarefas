using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using System.Linq.Expressions; // Para construir predicados dinâmicos

namespace UserManagement.API.Services;

public class TarefaService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TarefaService> _logger;

    public TarefaService(AppDbContext context, ILogger<TarefaService> logger)
    {
        _context = context;
        _logger = logger;
    }

    private static TarefaDto.TarefaResponse MapTarefaToResponseDto(Tarefa tarefa)
    {
        return new TarefaDto.TarefaResponse
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Descricao = tarefa.Descricao,
            DataAgendamento = tarefa.DataAgendamento,
            DataLimiteFinalizacao = tarefa.DataLimiteFinalizacao,
            Status = tarefa.Status.ToString(),
            EvidenciaUrl = tarefa.EvidenciaUrl,
            DataCriacao = tarefa.DataCriacao,
            DataConclusao = tarefa.DataConclusao,
            ResponsavelId = tarefa.ResponsavelId,
            NomeResponsavel = tarefa.Responsavel?.Nome ?? "N/A",
            CriadorId = tarefa.CriadorId,
            NomeCriador = tarefa.Criador?.Nome ?? "N/A",
            SetorId = tarefa.SetorId,
            NomeSetor = tarefa.Setor?.Nome ?? "N/A"
        };
    }

    // Método base para consultas de tarefas, para evitar repetição de Includes
    private IQueryable<Tarefa> GetTarefasBaseQuery()
    {
        return _context.Tarefas
            .Include(t => t.Responsavel)
            .Include(t => t.Criador)
            .Include(t => t.Setor)
            .AsNoTracking(); // Boa prática para consultas de leitura apenas
    }

    // 3 - Consulta a agenda de tarefas:
    // Sistema deve permitir a visualização de todas as tarefas agendadas.
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasAgendadasAsync(int? setorIdFiltro = null)
    {
        _logger.LogInformation("Buscando tarefas agendadas. Filtro de setor: {SetorId}", setorIdFiltro);
        var query = GetTarefasBaseQuery().Where(t => t.Status == StatusTarefa.Agendada || t.Status == StatusTarefa.EmAndamento);

        if (setorIdFiltro.HasValue)
        {
            query = query.Where(t => t.SetorId == setorIdFiltro.Value);
        }

        var tarefas = await query.ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }

    // Sistema deve permitir a visualização das tarefas do dia.
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoDiaAsync(DateTime? diaEspecifico = null, int? setorIdFiltro = null)
    {
        var hoje = diaEspecifico?.Date ?? DateTime.UtcNow.Date; // Usa o dia fornecido ou o dia atual (UTC)
        _logger.LogInformation("Buscando tarefas do dia: {Dia}. Filtro de setor: {SetorId}", hoje, setorIdFiltro);

        var query = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Date == hoje);

        if (setorIdFiltro.HasValue)
        {
            query = query.Where(t => t.SetorId == setorIdFiltro.Value);
        }

        var tarefas = await query.OrderBy(t => t.DataAgendamento).ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }

    // Sistema deve permitir a visualização das tarefas do dia por setor. (Combinação dos dois acima)
    
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoDiaPorSetorAsync(int setorId, DateTime? diaEspecifico = null)
    {
        _logger.LogInformation("Buscando tarefas do dia por setor. Setor: {SetorId}", setorId);
        return await GetTarefasDoDiaAsync(diaEspecifico, setorId);
    }


    // Sistema deve permitir a visualização das tarefas por semana.
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDaSemanaAsync(DateTime? dataInicioSemana = null, int? setorIdFiltro = null)
    {
        var inicioSemana = dataInicioSemana?.Date ?? DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek); // Domingo como início
        var fimSemana = inicioSemana.AddDays(6);
        _logger.LogInformation("Buscando tarefas da semana de {InicioSemana} a {FimSemana}. Filtro de setor: {SetorId}", inicioSemana, fimSemana, setorIdFiltro);

        var query = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Date >= inicioSemana && t.DataAgendamento.Date <= fimSemana);

        if (setorIdFiltro.HasValue)
        {
            query = query.Where(t => t.SetorId == setorIdFiltro.Value);
        }

        var tarefas = await query.OrderBy(t => t.DataAgendamento).ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }

    // Sistema deve permitir a visualização das tarefas por semana e por setor. (Combinação)
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDaSemanaPorSetorAsync(int setorId, DateTime? dataInicioSemana = null)
    {
        _logger.LogInformation("Buscando tarefas da semana por setor. Setor: {SetorId}", setorId);
        return await GetTarefasDaSemanaAsync(dataInicioSemana, setorId);
    }

    // Sistema deve permitir a visualização das tarefas por mês e por setor.
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoMesPorSetorAsync(int ano, int mes, int setorId)
    {
        if (mes < 1 || mes > 12) throw new ArgumentOutOfRangeException(nameof(mes), "Mês deve ser entre 1 e 12.");
        _logger.LogInformation("Buscando tarefas do mês {Mes}/{Ano} para o setor {SetorId}", mes, ano, setorId);

        var query = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Year == ano && t.DataAgendamento.Month == mes && t.SetorId == setorId);

        var tarefas = await query.OrderBy(t => t.DataAgendamento).ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }

    // Sistema deve permitir a visualização das tarefas por mês (geral, pode ser filtrado por setor no controller/frontend)
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoMesAsync(int ano, int mes, int? setorIdFiltro = null)
    {
        if (mes < 1 || mes > 12) throw new ArgumentOutOfRangeException(nameof(mes), "Mês deve ser entre 1 e 12.");
        _logger.LogInformation("Buscando tarefas do mês {Mes}/{Ano}. Filtro de setor: {SetorId}", mes, ano, setorIdFiltro);

        var query = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Year == ano && t.DataAgendamento.Month == mes);

        if (setorIdFiltro.HasValue)
        {
            query = query.Where(t => t.SetorId == setorIdFiltro.Value);
        }

        var tarefas = await query.OrderBy(t => t.DataAgendamento).ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }


    // Sistema deve permitir a visualização das tarefas por status (agendada, realizada, cancelada).
    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasPorStatusAsync(StatusTarefa status, int? setorIdFiltro = null)
    {
        _logger.LogInformation("Buscando tarefas com status {Status}. Filtro de setor: {SetorId}", status, setorIdFiltro);
        var query = GetTarefasBaseQuery().Where(t => t.Status == status);

        if (setorIdFiltro.HasValue)
        {
            query = query.Where(t => t.SetorId == setorIdFiltro.Value);
        }

        var tarefas = await query.OrderByDescending(t => t.DataAgendamento).ToListAsync();
        return tarefas.Select(MapTarefaToResponseDto);
    }

    // Método para buscar uma tarefa específica por ID (útil para o Módulo 2, mas bom ter)
    public async Task<TarefaDto.TarefaResponse?> GetTarefaByIdAsync(int id)
    {
        _logger.LogInformation("Buscando tarefa com ID: {Id}", id);
        var tarefa = await GetTarefasBaseQuery().FirstOrDefaultAsync(t => t.Id == id);

        return tarefa == null ? null : MapTarefaToResponseDto(tarefa);
    }
}