using Microsoft.EntityFrameworkCore;
using UserManagement.API.Models;

namespace UserManagement.API.Data;

public class AppDbContext : DbContext
{
	public DbSet<Setor> Setores { get; set; }
	public DbSet<Tarefa> Tarefas { get; set; }
    public DbSet<Loja> Lojas { get; set; }


    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Tarefa>()
			.HasOne(t => t.Setor)
			.WithMany(s => s.Tarefas)
			.HasForeignKey(t => t.SetorId);

        modelBuilder.Entity<Tarefa>()
             .HasOne(t => t.Loja)
             .WithMany(l => l.Tarefas)
             .HasForeignKey(t => t.LojaId)
             .OnDelete(DeleteBehavior.Cascade);
    }
}
