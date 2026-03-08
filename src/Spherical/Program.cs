using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MudBlazor.Services;
using Spherical;
using Spherical.Authentication;
using Spherical.Client.Services;
using Spherical.Core.Creative.Models;
using Spherical.Infrastructure.EF.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
// DbContext
builder.Services.AddDbContext<SphericalContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion")));

// JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtSettings["Issuer"],
            ValidAudience = jwtSettings["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(jwtSettings["Key"] ?? "Una_Clave_Secreta_Para_Generar_JWT"))
        };
    });

builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configurar HttpClient para servicios
builder.Services.AddHttpClient<ISecurityService, SecurityService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IMenuService, MenuService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IElementoService, ElementoService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IBodegaService, BodegaService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IDocumentoService, DocumentoService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient<IPermisoService, PermisoService>(client =>
{
    var apiUrl = builder.Configuration.GetValue<string>("AppSettings:ApiUrl") ?? "https://localhost:7079/";
    if (!apiUrl.EndsWith("/")) apiUrl += "/";
    client.BaseAddress = new Uri(apiUrl);
    client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddAuthorizationCore();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();