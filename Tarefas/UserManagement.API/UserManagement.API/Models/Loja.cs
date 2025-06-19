namespace UserManagement.API.Models
{
    public class Loja
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        // Relacionamento: uma loja pode ter várias tarefas
        public ICollection<Tarefa>? Tarefas { get; set; }
    }



}
