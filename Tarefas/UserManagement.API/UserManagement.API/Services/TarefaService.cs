using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using Microsoft.EntityFrameworkCore;

namespace UserManagement.API.Services;

public class TarefaService
{
    private readonly AppDbContext _context;

    public TarefaService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TarefaDto.Response> CreateTarefa(TarefaDto.CreateRequest dto)
    {
        // Valida se setor existe
        var setorExiste = await _context.Setores.AnyAsync(s => s.Id == dto.SetorId);
        if (!setorExiste)
            throw new ArgumentException("Setor informado não existe.");
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("Nome da tarefa é obrigatório.");

        var tarefa = new Tarefa
        {
            Nome = dto.Nome,
            Descricao = dto.Descricao,
            HorarioFinalizacao = dto.HorarioFinalizacao,
            Status = "Pendente",
            Evidencia = dto.Evidencia ?? string.Empty,
            Peso = dto.Peso.HasValue ? (dto.Peso.Value > 0 ? dto.Peso.Value : 1) : 1,
            SetorId = dto.SetorId
        };

        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return new TarefaDto.Response
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Descricao = tarefa.Descricao,
            HorarioFinalizacao = tarefa.HorarioFinalizacao,
            Status = tarefa.Status,
            Evidencia = tarefa.Evidencia,
            Peso = tarefa.Peso,
            SetorId = tarefa.SetorId
        };
    }

    public async Task<TarefaDto.Response?> GetTarefaById(int id)
    {
        var tarefa = await _context.Tarefas
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);

        if (tarefa == null) return null;

        return new TarefaDto.Response
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Descricao = tarefa.Descricao,
            HorarioFinalizacao = tarefa.HorarioFinalizacao,
            Status = tarefa.Status,
            Evidencia = tarefa.Evidencia,
            Peso = tarefa.Peso,
            SetorId = tarefa.SetorId
        };
    }

    public async Task AtualizarTarefaAsync(int id, TarefaDto.UpdateRequest dto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);

        if (tarefa == null)
            throw new Exception("Tarefa não encontrada.");

        // Atualiza Setor se informado e diferente
        if (dto.SetorId != null && dto.SetorId != tarefa.SetorId)
        {
            var setorExiste = await _context.Setores.AnyAsync(s => s.Id == dto.SetorId);
            if (!setorExiste)
                throw new ArgumentException("Setor informado não existe.");
            tarefa.SetorId = dto.SetorId.Value;
        }

        if (!string.IsNullOrWhiteSpace(dto.Nome))
            tarefa.Nome = dto.Nome;

        if (!string.IsNullOrWhiteSpace(dto.Descricao))
            tarefa.Descricao = dto.Descricao;

        if (dto.HorarioFinalizacao != null)
            tarefa.HorarioFinalizacao = dto.HorarioFinalizacao.Value;

        if (!string.IsNullOrWhiteSpace(dto.Status))
            tarefa.Status = dto.Status;

        if (!string.IsNullOrWhiteSpace(dto.Evidencia))
            tarefa.Evidencia = dto.Evidencia;

        if (dto.Peso != null)
            tarefa.Peso = dto.Peso.Value;

        _context.Tarefas.Update(tarefa);
        await _context.SaveChangesAsync();
    }

    public async Task CancelarTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null)
            throw new KeyNotFoundException("Tarefa não encontrada!");

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
    }
}
