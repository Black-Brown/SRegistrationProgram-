using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SRegisterApp.Data;
using System.IO;

var options = new WebApplicationOptions
{
    Args = args,
    WebRootPath = "wwwroot" // Especificamos manualmente la carpeta de archivos estáticos
};

var builder = WebApplication.CreateBuilder(options);

// Asegurar que la carpeta wwwroot exista
var wwwrootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
if (!Directory.Exists(wwwrootPath))
{
    Directory.CreateDirectory(wwwrootPath);
}

// Cargar configuración de appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

// Configurar la base de datos
builder.Services.AddDbContext<SRegisterAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SRegisterAppContext")
    ?? throw new InvalidOperationException("Connection string 'SRegisterAppContext' not found.")));

// Agregar servicios al contenedor
builder.Services.AddControllersWithViews();

builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(5000); // Puerto HTTP
    options.ListenAnyIP(5001, listenOptions => listenOptions.UseHttps()); // Puerto HTTPS
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
