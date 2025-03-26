using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SRegisterApp.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SRegisterAppContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SRegisterAppContext")
    ?? throw new InvalidOperationException("Connection string 'SRegisterAppContext' not found.")));

// ✅ Agregar HttpClient al contenedor de dependencias
builder.Services.AddHttpClient();

// Agregar controladores con vistas
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configurar el pipeline de solicitudes HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
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
