using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models; 
using Pomelo.EntityFrameworkCore.MySql.Infrastructure; 
using UserManagement.API.Data;
using UserManagement.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços
builder.Services.AddControllers();

// Configuração do Swagger
builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "UserManagement API", Version = "v1" });

    
    c.CustomSchemaIds(type => type.FullName?.Replace("+", "."));

    
});


// Configuração do MySQL/MariaDB
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

var serverVersion = new MariaDbServerVersion(new Version(10, 4, 32)); 

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(connectionString, serverVersion, mysqlOptions => 
    {
        mysqlOptions.SchemaBehavior(MySqlSchemaBehavior.Ignore); 
        mysqlOptions.EnableRetryOnFailure(
            maxRetryCount: 5,
            maxRetryDelay: TimeSpan.FromSeconds(30),
            errorNumbersToAdd: null); 
    })
);


// Injeção de Dependência dos Serviços
builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PerfilService>();
builder.Services.AddScoped<TarefaService>();

builder.Services.AddLogging();



var app = builder.Build();

// Pipeline de requisições HTTP
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