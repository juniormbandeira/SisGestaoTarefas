using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; // Adicione este using
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; // Para MySqlSchemaBehavior e MariaDbServerVersion
using UserManagement.API.Data;
using UserManagement.API.Services; // Adicione este using

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
builder.Services.AddControllers();

// Configuração do Swagger mais robusta
builder.Services.AddEndpointsApiExplorer(); // Necessário para controllers mínimos
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1" });

    // Adicione esta configuração para resolver o conflito de nomes em DTOs aninhados:
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

    // Opcional: Organize os controllers por tags (nome do controller)
    // c.TagActionsBy(api => new[] { api.GroupName ?? api.ActionDescriptor.RouteValues["controller"]?.ToString() });
});


// Configuração do MySQL/MariaDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// Use ServerVersion.AutoDetect(connectionString) para auto-deteção ou especifique a versão
// A versão do MariaDB (10, 4, 32) é um exemplo, use a sua.
var serverVersion = new MariaDbServerVersion(new Version(10, 4, 32)); // Ou use ServerVersion.AutoDetect(connectionString) para MySql

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, mysqlOptions => // Alterado para serverVersion genérico
    {
        mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore); // Opcional
        mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null); // Adicione explicitamente 'null' se não for adicionar outros
    })
);


// Injeção de Dependência dos Serviços
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<TarefaService>();
// Adicione ILogger se for usar explicitamente nos serviços
builder.Services.AddLogging();



var app = builder.Build();

// Pipeline de requisições HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "User Management API v1");
        c.RoutePrefix = "swagger"; // Define a rota como /swagger
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();