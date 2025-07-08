using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;

namespace UserManagement.API.Services;

public class TarefaService
{
    private readonly AppDbContext _context;
    private readonly ILogger<TarefaService> _logger;

    public TarefaService(AppDbContext context, ILogger<TarefaService> logger)
    {
        _context = context;
        _logger  = logger;
    }

    // ---------- MAPA DTO ----------
    private static TarefaDto.TarefaResponse MapTarefaToResponseDto(Tarefa tarefa) => new()
    {
        Id                  = tarefa.Id,
        Nome                = tarefa.Nome,
        Descricao           = tarefa.Descricao,
        DataAgendamento     = tarefa.DataAgendamento,
        DataLimiteFinalizacao = tarefa.DataLimiteFinalizacao,
        Peso                = tarefa.Peso,
        Status              = tarefa.Status.ToString(),
        EvidenciaUrl        = tarefa.EvidenciaUrl,
        DataCriacao         = tarefa.DataCriacao,
        DataConclusao       = tarefa.DataConclusao,

        ResponsavelId       = tarefa.ResponsavelId,
        NomeResponsavel     = tarefa.Responsavel?.Nome ?? "N/A",

        CriadorId           = tarefa.CriadorId,
        NomeCriador         = tarefa.Criador?.Nome ?? "N/A",

        SetorId             = tarefa.SetorId,
        NomeSetor           = tarefa.Setor?.Nome ?? "N/A",

        LojaId              = tarefa.LojaId,
        NomeLoja            = tarefa.Loja?.Nome ?? "N/A"
    };

    // ---------- QUERY BASE ----------
    private IQueryable<Tarefa> GetTarefasBaseQuery() =>
        _context.Tarefas
                .Include(t => t.Responsavel)
                .Include(t => t.Criador)
                .Include(t => t.Setor)
                .Include(t => t.Loja)
                .AsNoTracking();

    // ---------- CREATE ----------
    public async Task<TarefaDto.TarefaResponse> CreateTarefaAsync(
        TarefaDto.TarefaCreateRequest dto, int criadorId)
    {
        if (!await _context.Setores.AnyAsync(s => s.Id == dto.SetorId))
            throw new ArgumentException("Setor informado não existe.");

        if (!await _context.Lojas.AnyAsync(l => l.Id == dto.LojaId))
            throw new ArgumentException("Loja informada não existe.");

        if (dto.DataAgendamento >= dto.DataLimiteFinalizacao)
            throw new ArgumentException(
                "Data de agendamento não pode ser maior que a data-limite de finalização.");

        var tarefa = new Tarefa
        {
            Nome                   = dto.Nome,
            Descricao              = dto.Descricao,
            DataAgendamento        = dto.DataAgendamento,
            DataLimiteFinalizacao  = dto.DataLimiteFinalizacao,
            Status                 = StatusTarefa.Agendada,
            Peso                   = dto.Peso,
            EvidenciaUrl           = string.Empty,
            ResponsavelId          = dto.ResponsavelId,
            CriadorId              = criadorId,
            SetorId                = dto.SetorId,
            LojaId                 = dto.LojaId
        };

        if (tarefa.Status == StatusTarefa.Realizada)
            tarefa.DataConclusao = DateTime.UtcNow;

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return MapTarefaToResponseDto(tarefa);
    }

    // ---------- UPDATE ----------
    public async Task AtualizarTarefaAsync(int id, TarefaDto.TarefaUpdateRequest dto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id)
                     ?? throw new KeyNotFoundException("Tarefa não encontrada.");

        tarefa.Nome                  = dto.Nome;
        tarefa.Descricao             = dto.Descricao;
        tarefa.DataAgendamento       = dto.DataAgendamento;
        tarefa.DataLimiteFinalizacao = dto.DataLimiteFinalizacao;
        tarefa.Status                = dto.Status;
        tarefa.EvidenciaUrl          = dto.EvidenciaUrl ?? tarefa.EvidenciaUrl;
        tarefa.Peso                  = dto.Peso;
        tarefa.ResponsavelId         = dto.ResponsavelId;
        tarefa.SetorId               = dto.SetorId;
        tarefa.LojaId                = dto.LojaId;

        if (tarefa.DataAgendamento >= tarefa.DataLimiteFinalizacao)
            throw new ArgumentException(
                "Data de agendamento não pode ser maior que a data-limite de finalização.");

        if (tarefa.Status == StatusTarefa.Realizada && tarefa.DataConclusao == null)
            tarefa.DataConclusao = DateTime.UtcNow;
        else if (tarefa.Status != StatusTarefa.Realizada)
            tarefa.DataConclusao = null;

        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
    }

    // ---------- DELETE ----------
    public async Task CancelarTarefaAsync(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id)
                     ?? throw new KeyNotFoundException("Tarefa não encontrada!");

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
    }

    // ---------- READ ----------
    public async Task<TarefaDto.TarefaResponse?> GetTarefaByIdAsync(int id)
    {
        var tarefa = await GetTarefasBaseQuery().FirstOrDefaultAsync(t => t.Id == id);
        return tarefa == null ? null : MapTarefaToResponseDto(tarefa);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasAgendadasAsync(int? setorId = null)
    {
        var q = GetTarefasBaseQuery()
            .Where(t => t.Status == StatusTarefa.Agendada || t.Status == StatusTarefa.EmAndamento);

        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);

        return (await q.ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoDiaAsync(DateTime? dia = null, int? setorId = null)
    {
        var data = dia?.Date ?? DateTime.UtcNow.Date;

        var q = GetTarefasBaseQuery().Where(t => t.DataAgendamento.Date == data);

        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);

        return (await q.OrderBy(t => t.DataAgendamento).ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDaSemanaAsync(DateTime? inicioSemana = null, int? setorId = null)
    {
        var inicio = inicioSemana?.Date ?? DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
        var fim    = inicio.AddDays(6);

        var q = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Date >= inicio && t.DataAgendamento.Date <= fim);

        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);

        return (await q.OrderBy(t => t.DataAgendamento).ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoMesAsync(int ano, int mes, int? setorId = null)
    {
        if (mes < 1 || mes > 12)
            throw new ArgumentOutOfRangeException(nameof(mes));

        var q = GetTarefasBaseQuery()
            .Where(t => t.DataAgendamento.Year == ano && t.DataAgendamento.Month == mes);

        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);

        return (await q.OrderBy(t => t.DataAgendamento).ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasPorStatusAsync(StatusTarefa status, int? setorId = null)
    {
        var q = GetTarefasBaseQuery().Where(t => t.Status == status);
        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);
        return (await q.OrderByDescending(t => t.DataAgendamento).ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasRankeadasAsync(int? setorId = null)
    {
        var q = GetTarefasBaseQuery();
        if (setorId.HasValue) q = q.Where(t => t.SetorId == setorId);
        return (await q.OrderByDescending(t => t.Peso).ToListAsync()).Select(MapTarefaToResponseDto);
    }

    public async Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasPorLojaAsync(int lojaId)
    {
        var tarefas = await GetTarefasBaseQuery()
            .Where(t => t.LojaId == lojaId && t.Loja != null)
            .OrderByDescending(t => t.DataAgendamento)
            .ToListAsync();

        return tarefas.Select(MapTarefaToResponseDto);
    }

    // ---------- ATALHOS ----------
    public Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDoDiaPorSetorAsync(int setorId, DateTime? dia = null) =>
        GetTarefasDoDiaAsync(dia, setorId);

    public Task<IEnumerable<TarefaDto.TarefaResponse>> GetTarefasDaSemanaPorSetorAsync(int setorId, DateTime? inicioSemana = null) =>
        GetTarefasDaSemanaAsync(inicioSemana, setorId);
}
