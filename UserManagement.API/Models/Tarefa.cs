using System.ComponentModel.DataAnnotations; 

namespace UserManagement.API.Models; 


public enum StatusTarefa
{
    [Display(Name = "Agendada")]
    Agendada,

    [Display(Name = "Em Andamento")]
    EmAndamento,

    [Display(Name = "Realizada")]
    Realizada,

    [Display(Name = "Cancelada")]
    Cancelada,

    [Display(Name = "Pendente Aprovação")]
    PendenteAprovacao,

    [Display(Name = "Urgente")]
    Urgente
}
public class Tarefa
{
    public int Id { get; set; }
    public required string Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime DataAgendamento { get; set; } // Data em que a tarefa deve ser iniciada/focada
    public DateTime DataLimiteFinalizacao { get; set; } // Prazo final
    public int Peso { get; set; } = 1; // Peso para rankeamento
    public StatusTarefa Status { get; set; } = StatusTarefa.Agendada; // Valor padrão
    public string? EvidenciaUrl { get; set; } // Para link/path da foto
    public DateTime DataCriacao { get; set; } = DateTime.UtcNow;
    public DateTime? DataConclusao { get; set; } // Quando foi efetivamente concluída

    // Chave Estrangeira para o Usuário responsável pela execução
    public int ResponsavelId { get; set; }
    public User Responsavel { get; set; } = null!; // O "null!" indica ao compilador que será inicializado

    // Chave Estrangeira para o Usuário que criou a tarefa
    public int CriadorId { get; set; }
    public User Criador { get; set; } = null!;

    // Chave Estrangeira para o Setor ao qual a tarefa pertence
    public int SetorId { get; set; }
    public Setor Setor { get; set; } = null!;

    // Chave Estrangeira para a Loja relacionada

    public int LojaId { get; set; } = 1;
    public Loja Loja { get; set; } = null!;
}
