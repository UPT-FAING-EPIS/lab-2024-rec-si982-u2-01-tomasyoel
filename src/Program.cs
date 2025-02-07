using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shorten.Areas.Identity.Data;
using Shorten.Areas.Domain;

var builder = WebApplication.CreateBuilder(args);

// Cadena de conexión desde appsettings.json
var connectionString = builder.Configuration.GetConnectionString("ShortenIdentityDbContextConnection") 
                       ?? throw new InvalidOperationException("Connection string 'ShortenIdentityDbContextConnection' not found.");

// Configuración del contexto de Identity
builder.Services.AddDbContext<ShortenIdentityDbContext>(options => 
    options.UseSqlServer(connectionString));

// Configuración de la autenticación y autorización
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ShortenIdentityDbContext>();

// Configuración del contexto de dominio (ShortenContext)
builder.Services.AddDbContext<ShortenContext>(options => 
    options.UseSqlServer(connectionString));

// Agregar soporte para Razor Pages
builder.Services.AddRazorPages();

var app = builder.Build();

// Configuración del pipeline de la aplicación
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
