using Microsoft.EntityFrameworkCore;
using SRegisterApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

// ?? Registrar el DbContext
builder.Services.AddDbContext<SRegisterAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SRegisterAppContext") ??
        throw new InvalidOperationException("Connection string 'SRegisterAppContext' not found.")));

// Agregar controladores
builder.Services.AddControllers();

// Agregar Swagger (si lo estás usando)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Habilitar Swagger en desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
