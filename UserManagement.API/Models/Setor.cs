namespace UserManagement.API.Models;

public class Setor
{
    public int Id { get; set; }
    public required string Nome { get; set; } //  "Financeiro", "RH", "Desenvolvimento"
    public string? Descricao { get; set; }

    // Um setor pode ter vários usuários
    public List<User> Usuarios { get; set; } = new();

    // Um setor pode ter várias tarefas
    public List<Tarefa> Tarefas { get; set; } = new();
}