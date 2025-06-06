using UserManagement.API.Data;
using UserManagement.API.Models;

namespace UserManagement.API.Services;

public class UserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    // Serviços de exemplo
}
