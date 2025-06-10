namespace UserManagement.API.Models;

public class User
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    // Adicione o campo CPF se ele for obrigatório para login, conforme os requisitos
    // public required string Cpf { get; set; } 
    public required string Email { get; set; } // Ou 'Login' se preferirem usar um username
    public required string SenhaHash { get; set; }
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;

    // Chave estrangeira para Perfil (já existe do tutorial anterior)
    public int PerfilId { get; set; }
    public Perfil? Perfil { get; set; }

    // Nova: Chave estrangeira para Setor
    public int? SetorId { get; set; } // Pode ser nulo se nem todo usuário pertencer a um setor específico
    public Setor? Setor { get; set; }

    // Propriedades de navegação para Tarefas
    // Tarefas pelas quais este usuário é o responsável
    public List<Tarefa> TarefasComoResponsavel { get; set; } = new();

    // Tarefas que este usuário criou
    public List<Tarefa> TarefasComoCriador { get; set; } = new();
}