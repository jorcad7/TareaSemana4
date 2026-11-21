using Microsoft.EntityFrameworkCore;
using Sistema.Models;

var builder = WebApplication.CreateBuilder(args);

// 1. Registrar servicios MVC (controladores y vistas)
builder.Services.AddControllersWithViews();

// 2. Leer la cadena de conexión "conexion" desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("conexion")
    ?? throw new InvalidOperationException("No se encontró la cadena de conexión 'conexion' en appsettings.json.");

// 3. Registrar el DbContext con el proveedor MySQL
builder.Services.AddDbContext<SistemaAcademicoContext>(options =>
    options.UseMySQL(connectionString));

var app = builder.Build();

// 4. Configurar el pipeline HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
