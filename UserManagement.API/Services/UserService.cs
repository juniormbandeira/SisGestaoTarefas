using Microsoft.EntityFrameworkCore;
using UserManagement.API.Data;
using UserManagement.API.DTOs;
using UserManagement.API.Models;
using BCrypt.Net; 

namespace UserManagement.API.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Método para criar usuário
    public async Task<UserDto.Response> CreateUser(UserDto.CreateRequest dto)
    {
        // Validações adicionais
        if (await _context.Users.AnyAsync(u => u.Email == dto.Email))
            throw new Exception("Email já cadastrado!");

        // Mapear DTO para a entidade User
        var user = new User
        {
            Nome = dto.Nome,
            Email = dto.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(dto.Senha),
            DataCriacao = DateTime.UtcNow,
            PerfilId = 2 
                         
        };

        // Salvar no banco
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        // Retornar DTO de resposta
        return new UserDto.Response
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            DataCriacao = user.DataCriacao,
            PerfilId = user.PerfilId
        };
    }

    public async Task<UserDto.Response?> GetUserById(int id)
    {
        // Busca o usuário no banco de dados pelo Id 
        var user = await _context.Users
            
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
            return null; // Retorna null se o usuário não existir

        // Mapeia a entidade User para UserDto.Response
        return new UserDto.Response
        {
            Id = user.Id,
            Nome = user.Nome,
            Email = user.Email,
            DataCriacao = user.DataCriacao,
            PerfilId = user.PerfilId 
        };
    }

   
}