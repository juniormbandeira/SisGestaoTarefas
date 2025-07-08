namespace UserManagement.API.DTOs;
using UserManagement.API.Models;

public static class TarefaDto
{
    // DTO para retornar informações de uma tarefa
    public class TarefaResponse
    {
        public int Id { get; set; }
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataLimiteFinalizacao { get; set; }
        public int Peso { get; set; }
        public string Status { get; set; } = string.Empty; 
        public string? EvidenciaUrl { get; set; }
        public DateTime DataCriacao { get; set; }
        public DateTime? DataConclusao { get; set; }

        public int ResponsavelId { get; set; }
        public string NomeResponsavel { get; set; } = string.Empty;

        public int CriadorId { get; set; }
        public string NomeCriador { get; set; } = string.Empty;

        public int SetorId { get; set; }
        public string NomeSetor { get; set; } = string.Empty;

        public int LojaId { get; set; }
        public string NomeLoja { get; set; } = string.Empty;
    }

    // DTO para filtros de consulta
    public class TarefaQueryParameters
    {
        public int? SetorId { get; set; }
        public int? ResponsavelId { get; set; }
        public string? Status { get; set; } 
        public DateTime? DataEspecifica { get; set; } 
        public DateTime? DataInicioSemana { get; set; }
        public DateTime? DataFimSemana { get; set; }
        public int? Mes { get; set; }
        public int? Ano { get; set; }
    }

    // DTO para criação de tarefa 
    public class TarefaCreateRequest
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataLimiteFinalizacao { get; set; }
        
        public int Peso { get; set; } = 1;
        public int ResponsavelId { get; set; }
        public int SetorId { get; set; }
        public int LojaId { get; set; }
        
    }

    // DTO para atualização de tarefa (Módulo 2)
    public class TarefaUpdateRequest
    {
        public required string Nome { get; set; }
        public string? Descricao { get; set; }
        public DateTime DataAgendamento { get; set; }
        public DateTime DataLimiteFinalizacao { get; set; }
        public StatusTarefa Status { get; set; }
        public string? EvidenciaUrl { get; set; }
        public int Peso { get; set; } = 1;
        public int ResponsavelId { get; set; }
        public int SetorId { get; set; }
        public int LojaId { get; set; }
    }
}
