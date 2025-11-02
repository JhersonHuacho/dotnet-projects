using Blazored.SessionStorage;
using Blazored.Toast;
using CurrieTechnologies.Razor.SweetAlert2;
using Huachin.MusicStore.AccesoDatos.Contexto;
using Huachin.MusicStore.AccesoDatos.Seguridad;
using Huachin.MusicStore.Repositorios.Implementaciones;
using Huachin.MusicStore.Repositorios.Interfaces;
using Huachin.MusicStore.Servicio.Implementaciones;
using Huachin.MusicStore.Servicio.Interfaces;
using Huachin.MusicStore.UI.Components;
using Huachin.MusicStore.UI.ConfigRutas;
using Huachin.MusicStore.UI.Servicios;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

//builder.Services.Configure<CircuitOptions>(options =>
//{
//	options.DetailedErrors = true;
//});

builder.Services.AddDbContext<MusicStoreContext>(options => 
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BdPedidos"));
});

#region Seguridad

builder.Services.AddDbContext<BdSeguridadContext>(options =>
{
	options.UseNpgsql(builder.Configuration.GetConnectionString("BdSeguridad"));
});

builder.Services.AddIdentity<SeguridadEntity, IdentityRole>(politica =>
{
	politica.Password.RequireDigit = false;
	politica.Password.RequireUppercase = true;
	politica.Password.RequireLowercase = true;
	politica.Password.RequiredLength = 8;
	politica.Password.RequireNonAlphanumeric = false;
	politica.User.RequireUniqueEmail = true;
})
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<BdSeguridadContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
	options.LoginPath = Rutas.Login;
});

builder.Services.AddBlazoredSessionStorage();

builder.Services.AddScoped<AutenticacionService>();
builder.Services.AddScoped<AuthenticationStateProvider>(provider =>
{
	return provider.GetRequiredService<AutenticacionService>();
});

builder.Services.AddAuthorization();

#endregion


builder.Services.AddServerSideBlazor()
	.AddCircuitOptions(options =>
	{
		options.DetailedErrors = true;
		options.DisconnectedCircuitMaxRetained = 100;
		options.DisconnectedCircuitRetentionPeriod = TimeSpan.FromMinutes(3);
		options.JSInteropDefaultCallTimeout = TimeSpan.FromMinutes(3);
		options.MaxBufferedUnacknowledgedRenderBatches = 10;
	});

builder.Services.AddBlazorBootstrap();
builder.Services.AddBlazoredToast();
builder.Services.AddSweetAlert2();

builder.Services.AddScoped<IGenreRepositorio, GenreRepositorio>();
builder.Services.AddScoped<IGenreServicio, GenreServicio>();

builder.Services.AddScoped<IConcertRepositorio, ConcertRepositorio>();
builder.Services.AddScoped<IConcertServicio, ConcertServicio>();

builder.Services.AddScoped<ISaleRepositorio, SaleRepositorio>();
builder.Services.AddScoped<ISaleServicio, SaleServicio>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

#region Seguridad
using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	await SeedData.Inicializar(services);
}
#endregion

app.Run();
