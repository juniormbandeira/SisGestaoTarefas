using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.OpenApi.Models;
using UserManagement.API.Data;
using UserManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
	c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1" });
	c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));
});

// String de conexão com o MySQL
builder.Services.AddDbContext<AppDbContext>(options =>
	options.UseMySql(
		builder.Configuration.GetConnectionString("DefaultConnection"),
		new MySqlServerVersion(new Version(10, 4, 32)),
		mysqlOptions =>
		{
			mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore); // Opcional
			mysqlOptions.EnableRetryOnFailure(
				maxRetryCount: 5,
				maxRetryDelay: TimeSpan.FromSeconds(30),
				errorNumbersToAdd: null
			);
		}
	)
);

// Injeção de dependência dos serviços
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<TarefaService>();

var app = builder.Build();

// Configuração do Swagger no ambiente de desenvolvimento
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI(c =>
	{
		c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
		c.RoutePrefix = "swagger";
	});
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
