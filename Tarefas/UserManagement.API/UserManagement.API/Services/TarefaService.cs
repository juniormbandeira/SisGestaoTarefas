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
        var tarefa = new Tarefa
        {
            Nome = dto.Nome,
            Setor = dto.Setor,
            HorarioFinalizacao = dto.HorarioFinalizacao,
            Status = "Pendente"
        };
        _context.Tarefas.Add(tarefa);
        await _context.SaveChangesAsync();

        return new TarefaDto.Response
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Setor = tarefa.Setor,
            HorarioFinalizacao = tarefa.HorarioFinalizacao,
            Status = tarefa.Status
        };
    }

    public async Task<TarefaDto.Response?> GetTarefaById(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        return tarefa != null ? new TarefaDto.Response
        {
            Id = tarefa.Id,
            Nome = tarefa.Nome,
            Setor = tarefa.Setor,
            HorarioFinalizacao = tarefa.HorarioFinalizacao,
            Status = tarefa.Status,
            Evidencia = tarefa.Evidencia
        } : null;
    }

    public async Task UpdateTarefa(int id, TarefaDto.UpdateRequest dto)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada!");

        tarefa.Status = dto.Status ?? tarefa.Status;
        tarefa.Evidencia = dto.Evidencia ?? tarefa.Evidencia;

        await _context.SaveChangesAsync();
    }

    public async Task CancelarTarefa(int id)
    {
        var tarefa = await _context.Tarefas.FindAsync(id);
        if (tarefa == null) throw new KeyNotFoundException("Tarefa não encontrada!");

        _context.Tarefas.Remove(tarefa);
        await _context.SaveChangesAsync();
    }
}
