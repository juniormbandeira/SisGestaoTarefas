namespace UserManagement.API.Models;

public class Loja
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }

    // Navegação: uma loja possui várias tarefas
    public List<Tarefa> Tarefas { get; set; } = new();
}
