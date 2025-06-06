namespace UserManagement.API.Models
{
	public class Setor
	{
		public int Id { get; set; }

		public string Nome { get; set; } = string.Empty;

		// Navegação: um setor pode ter várias tarefas
		public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
	}
}
