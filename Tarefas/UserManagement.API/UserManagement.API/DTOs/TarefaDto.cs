using System.ComponentModel.DataAnnotations;
using UserManagement.API.Models;
namespace UserManagement.API.DTOs;

public static class TarefaDto
{
    public class CreateRequest
    {
        public int? id { get; set;}
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? HorarioFinalizacao { get; set; }
        public string? Status { get; set; }
        public string? Evidencia { get; set; }
        public int? Peso { get; set; }
        public int? SetorId { get; set; }  // Referência ao Setor
    }


    public class UpdateRequest
    {
        public int? id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? HorarioFinalizacao { get; set; }
        public string? Status { get; set; }
        public string? Evidencia { get; set; }
        public int? Peso { get; set; }
        public int? SetorId { get; set; }
    }

    public class Response
    {
        public int? Id { get; set; }  // Adicione isso
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime? HorarioFinalizacao { get; set; }
        public string? Status { get; set; }
        public string? Evidencia { get; set; }
        public int? Peso { get; set; }
        public int? SetorId { get; set; }
    }
}
