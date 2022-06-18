
using Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("SqliteConnectionString")
          ?? "Data Source=AgendaEscolarDb.db";
builder.Services.AddSqlite<AgendaEscolarDbContext>(connectionString);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
await AsseguraDBExiste(app.Services, app.Logger);

app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

async Task AsseguraDBExiste(IServiceProvider services, ILogger logger)
{
    logger.LogInformation("Garantindo que o banco de dados exista e esteja na string de conexão :" +
        " '{connectionString}'", connectionString);
    using var db = services.CreateScope().ServiceProvider.GetRequiredService<AgendaEscolarDbContext>();
    await db.Database.EnsureCreatedAsync();
}