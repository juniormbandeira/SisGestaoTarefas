using Microsoft.EntityFrameworkCore;
using UserManagement.API.Models; 

namespace UserManagement.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    // DbSets para as entidades que serão mapeadas para tabelas
    public DbSet<User> Users { get; set; }
    public DbSet<Perfil> Perfis { get; set; }
    public DbSet<Setor> Setores { get; set; }
    public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Loja> Lojas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Chama a implementação base

        // === CONFIGURAÇÕES DE RELACIONAMENTO ENTRE ENTIDADES ===

        // Relacionamento: User <-> Perfil (Um User tem um Perfil, um Perfil pode ter muitos Users)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Perfil) // Propriedade de navegação em User para Perfil
            .WithMany(p => p.Usuarios) // Propriedade de navegação em Perfil para a lista de Users
            .HasForeignKey(u => u.PerfilId) // Chave estrangeira em User
            .OnDelete(DeleteBehavior.Restrict); // Impede deletar um Perfil se ele tiver Users associados

        // Relacionamento: User <-> Setor (Um User pode ter um Setor, um Setor pode ter muitos Users)
        modelBuilder.Entity<User>()
            .HasOne(u => u.Setor) // Propriedade de navegação em User para Setor
            .WithMany(s => s.Usuarios) // Propriedade de navegação em Setor para a lista de Users
            .HasForeignKey(u => u.SetorId) // Chave estrangeira em User
            .IsRequired(false) // Torna SetorId opcional (anulável) para um User
            .OnDelete(DeleteBehavior.SetNull); // Se um Setor for deletado, o SetorId nos Users associados se torna NULL

        // Relacionamento: Tarefa <-> Setor (Uma Tarefa pertence a um Setor, um Setor pode ter muitas Tarefas)
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Setor) // Propriedade de navegação em Tarefa para Setor
            .WithMany(s => s.Tarefas) // Propriedade de navegação em Setor para a lista de Tarefas
            .HasForeignKey(t => t.SetorId) // Chave estrangeira em Tarefa
            .OnDelete(DeleteBehavior.Cascade); // Se um Setor for deletado, todas as suas Tarefas associadas também são deletadas.
                                               

        // Relacionamento: Tarefa <-> Loja (Uma Tarefa pertence a uma Loja, uma Loja pode ter muitas Tarefas)
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Loja)
            .WithMany(l => l.Tarefas)
            .HasForeignKey(t => t.LojaId)
            .OnDelete(DeleteBehavior.Cascade);

        // Valor padrão: se nenhuma loja for informada, usar a Matriz (Id = 1)
        modelBuilder.Entity<Tarefa>()
            .Property(t => t.LojaId)
            .HasDefaultValue(1);

        // Relacionamento: Tarefa <-> User (Responsável)
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Responsavel) // Propriedade de navegação em Tarefa para o User responsável
            .WithMany(u => u.TarefasComoResponsavel) // Propriedade de navegação em User para suas tarefas como responsável
            .HasForeignKey(t => t.ResponsavelId) // Chave estrangeira em Tarefa
            .OnDelete(DeleteBehavior.Restrict); // Impede deletar um User se ele for responsável por Tarefas

        // Relacionamento: Tarefa <-> User (Criador)
        modelBuilder.Entity<Tarefa>()
            .HasOne(t => t.Criador) // Propriedade de navegação em Tarefa para o User criador
            .WithMany(u => u.TarefasComoCriador) // Propriedade de navegação em User para suas tarefas como criador
            .HasForeignKey(t => t.CriadorId) // Chave estrangeira em Tarefa
            .OnDelete(DeleteBehavior.Restrict); // Impede deletar um User se ele criou Tarefas

       
       
        modelBuilder.Entity<Tarefa>()
            .Property(t => t.Status)
            .HasConversion<string>();

        // --- SEED DATA (DADOS INICIAIS) ---
        // O Seed Data para fins de teste.

        // 1. Seed inicial de Perfis (Essencial para o sistema funcionar)
        var perfilAdminId = 1;
        var perfilFuncionarioId = 2;
        var perfilGerenteId = 3;
       
       

        modelBuilder.Entity<Perfil>().HasData(
            new Perfil { Id = perfilAdminId, Nome = "Admin", Descricao = "Administrador do sistema" },
            new Perfil { Id = perfilFuncionarioId, Nome = "Funcionário", Descricao = "Funcionário do setor" },
            new Perfil { Id = perfilGerenteId, Nome = "Gerente", Descricao = "Gerente de operações" }
            
        
        );

        // 2. Seed inicial de Setores
        var setorDevId = 1;
        var setorRhId = 2;
        var setorFinanceiroId = 3;

        modelBuilder.Entity<Setor>().HasData(
           new Setor { Id = setorDevId, Nome = "Desenvolvimento", Descricao = "Setor de Desenvolvimento de Software" },
           new Setor { Id = setorRhId, Nome = "Recursos Humanos", Descricao = "Setor de Gestão de Pessoas" },
           new Setor { Id = setorFinanceiroId, Nome = "Financeiro", Descricao = "Setor de Finanças e Contabilidade" }
        );

        // Seed inicial de Lojas 
        var lojaMatrizId = 1;
        var lojaFilialAId = 2;

        modelBuilder.Entity<Loja>().HasData(
            new Loja { Id = lojaMatrizId, Nome = "Matriz", Descricao = "Loja principal" },
            new Loja { Id = lojaFilialAId, Nome = "Filial A", Descricao = "Primeira filial" }
        );

        // 3. Seed inicial de Usuários 

        var usuarioCoordenadorDevId = 1; 
        var usuarioFuncionarioDevId = 2;
        var usuarioFuncionarioRhId = 3; 


        // 4. Seed inicial de Tarefas 
        
            modelBuilder.Entity<Tarefa>().HasData(
            new Tarefa
            {
                Id = 1,
                Nome = "Revisar Documentação API V1",
                Descricao = "Verificar todos os endpoints e exemplos da documentação da API de usuários.",
                DataAgendamento = DateTime.UtcNow.Date,
                DataLimiteFinalizacao = DateTime.UtcNow.Date.AddDays(2),
                Peso = 3,
                Status = StatusTarefa.Agendada,
                ResponsavelId = usuarioFuncionarioDevId, 
                CriadorId = usuarioCoordenadorDevId,     
                SetorId = setorDevId,                    
                LojaId = lojaMatrizId
            },
            new Tarefa
            {
                Id = 2,
                Nome = "Preparar Relatório Semanal de Progresso",
                Descricao = "Compilar dados para o relatório de progresso da equipe de desenvolvimento.",
                DataAgendamento = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek).AddDays(1), 
                DataLimiteFinalizacao = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek).AddDays(4), 
                Peso = 2,
                Status = StatusTarefa.EmAndamento,
                ResponsavelId = usuarioFuncionarioDevId,
                CriadorId = usuarioCoordenadorDevId,
                SetorId = setorDevId,
                LojaId = lojaMatrizId
            },
            new Tarefa
            {
                Id = 3,
                Nome = "Planejamento da Sprint de Junho",
                Descricao = "Definir e estimar tarefas para a próxima sprint do projeto X.",
                DataAgendamento = new DateTime(DateTime.UtcNow.Year, 6, 1), 
                DataLimiteFinalizacao = new DateTime(DateTime.UtcNow.Year, 6, 5),
                Peso = 5,
                Status = StatusTarefa.Agendada,
                ResponsavelId = usuarioCoordenadorDevId,
                CriadorId = usuarioCoordenadorDevId, 
                SetorId = setorDevId,
                LojaId = lojaMatrizId
            },
            new Tarefa
            {
                Id = 4,
                Nome = "Entrevistas Candidatos Analista RH",
                Descricao = "Realizar entrevistas com os candidatos finalistas para a vaga de Analista de RH.",
                DataAgendamento = DateTime.UtcNow.Date.AddDays(1),
                DataLimiteFinalizacao = DateTime.UtcNow.Date.AddDays(3),
                Peso = 1,
                Status = StatusTarefa.Agendada,
                ResponsavelId = usuarioFuncionarioRhId, 
                CriadorId = usuarioCoordenadorDevId,    
                SetorId = setorRhId,                   
                LojaId = lojaFilialAId
            },
            new Tarefa
            {
                Id = 5,
                Nome = "Apresentar Resultados Trimestrais",
                Descricao = "Preparar e apresentar os resultados financeiros do último trimestre.",
                DataAgendamento = DateTime.UtcNow.Date.AddDays(10),
                DataLimiteFinalizacao = DateTime.UtcNow.Date.AddDays(15),
                Peso = 4,
                Status = StatusTarefa.Agendada,
                ResponsavelId = usuarioCoordenadorDevId, 
                CriadorId = usuarioCoordenadorDevId,     
                SetorId = setorFinanceiroId,
                LojaId = lojaFilialAId
            }
        );
    }
}
