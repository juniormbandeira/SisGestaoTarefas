using Microsoft.EntityFrameworkCore;
using UserManagement.API.Models;

namespace UserManagement.API.Data;

public class AppDbContext : DbContext
{
	public DbSet<Setor> Setores { get; set; }
	public DbSet<Tarefa> Tarefas { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<Tarefa>()
			.HasOne(t => t.Setor)
			.WithMany(s => s.Tarefas)
			.HasForeignKey(t => t.SetorId);
	}
}
