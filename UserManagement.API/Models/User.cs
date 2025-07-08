namespace UserManagement.API.Models;

public class User
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    
    
    public required string Email { get; set; } 
    public required string SenhaHash { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira para Perfil 
    public int PerfilId { get; set; }
    public Perfil? Perfil { get; set; }

    // Chave estrangeira para Setor 
    public int? SetorId { get; set; } 
    public Setor? Setor { get; set; }

   
    
    public List<Tarefa> TarefasComoResponsavel { get; set; } = new();

    
    public List<Tarefa> TarefasComoCriador { get; set; } = new();
}