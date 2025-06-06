using System.ComponentModel.DataAnnotations;
using UserManagement.API.Models;
namespace UserManagement.API.DTOs;

public static class TarefaDto
{
    public class CreateRequest
    {
        [Required]
        public required string Nome { get; set; }
        [Required]
        public required Setor Setor { get; set; }
        [Required]
 
        public DateTime? HorarioFinalizacao { get; set; }
    }

    public class UpdateRequest
    {
        [Required]
        public int Id { get; set; }
        public string? Status { get; set; }
        public string? Evidencia { get; set; }
    }

    public class Response
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public required Setor Setor { get; set; }
        public DateTime? HorarioFinalizacao { get; set; }
        public string? Status { get; set; }
        public string? Evidencia { get; set; }
    }
}
