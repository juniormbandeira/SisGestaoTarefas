using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Para ILogger
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models; // Para Perfil e User

namespace UserManagement.API.Services;

public class PerfilService
{
    private readonly AppDbContext _context;
    private readonly ILogger<PerfilService> _logger;

    public PerfilService(AppDbContext context, ILogger<PerfilService> logger)
    {
        _context = context;
        _logger = logger;
    }

    // Cria um novo perfil (vinculado a um User - o guia não mostra o UserId no DTO, mas o código sim)
    // Se o PerfilDto.CreateRequest não tiver UserId, remova o parâmetro dto.UserId abaixo
    public async Task<PerfilDto.Response> CreatePerfil(PerfilDto.CreateRequest dto)
    {
        try
        {
            
            // Por exemplo, um perfil pode ser criado sem usuários inicialmente.
            // var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == dto.UserId);
            // if (user == null && dto.UserId != 0) // Se UserId for opcional
            //    throw new KeyNotFoundException("Usuário não encontrado!");

            // Cria o perfil
            var perfil = new Perfil
            {
                Nome = dto.Nome,
                Descricao = dto.Descricao,
                // Usuarios = user != null ? new List<User> { user } : new List<User>() // Vincula ao User se encontrado
            };

            _context.Perfis.Add(perfil);
            await _context.SaveChangesAsync();

            return new PerfilDto.Response
            {
                Id = perfil.Id,
                Nome = perfil.Nome,
                Descricao = perfil.Descricao,
                Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
                {
                    Id = u.Id,
                    Nome = u.Nome,
                    Email = u.Email
                }).ToList()
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao criar perfil");
            throw;
        }
    }

    public async Task<PerfilDto.Response?> UpdatePerfil(int id, PerfilDto.UpdateRequest dto)
    {
        var perfil = await _context.Perfis
            .Include(p => p.Usuarios)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (perfil == null)
            throw new KeyNotFoundException("Perfil não encontrado!");

        perfil.Nome = dto.Nome;
        perfil.Descricao = dto.Descricao;

        await _context.SaveChangesAsync();

        return new PerfilDto.Response
        {
            Id = perfil.Id,
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList()
        };
    }

    public async Task<PerfilDto.Response?> GetPerfilById(int id)
    {
        var perfil = await _context.Perfis
            .Include(p => p.Usuarios)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (perfil == null)
            return null;

        return new PerfilDto.Response
        {
            Id = perfil.Id,
            Nome = perfil.Nome,
            Descricao = perfil.Descricao,
            Usuarios = perfil.Usuarios.Select(u => new PerfilDto.UserShortResponse
            {
                Id = u.Id,
                Nome = u.Nome,
                Email = u.Email
            }).ToList()
        };
    }

    public async Task DeletePerfil(int id) // Retorno void ou bool
    {
        var perfil = await _context.Perfis.FindAsync(id);
        if (perfil == null)
            throw new KeyNotFoundException("Perfil não encontrado!");

        // Verifique se há usuários associados se OnDelete.Restrict não for suficiente
        // ou se você quiser uma mensagem de erro mais amigável antes da exceção do DB.
        bool hasUsers = await _context.Users.AnyAsync(u => u.PerfilId == id);
        if (hasUsers)
        {
            throw new InvalidOperationException("Não é possível excluir o perfil pois existem usuários associados.");
        }

        _context.Perfis.Remove(perfil); // Hard delete
        await _context.SaveChangesAsync();
    }
}