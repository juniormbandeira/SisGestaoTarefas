namespace UserManagement.API.Models
{
	public class Tarefa
	{
		public int Id { get; set; }

		public string? Nome { get; set; } = string.Empty;

		public string? Descricao { get; set; } = string.Empty;

		public DateTime? HorarioFinalizacao { get; set; }

		public string? Status { get; set; } = string.Empty;

		public string? Evidencia { get; set; } = string.Empty;

        public int? Peso { get; set; } = 1;

        // Chave estrangeira
        public int? SetorId { get; set; }

		// Navegação
		public Setor Setor { get; set; } = null!;
	}
}
