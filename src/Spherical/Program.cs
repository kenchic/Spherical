using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor.Services;
using Spherical;
using Spherical.Authentication;
using Spherical.Client.Services;
using Spherical.Core.Creative.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// Configurar HttpClient para servicios
builder.Services.AddHttpClient<IElementoService, ElementoService>(client =>
{
    // Asegura que la BaseAddress esté configurada para que las URIs relativas funcionen
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
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();