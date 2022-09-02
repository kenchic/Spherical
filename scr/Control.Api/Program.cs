using Control.Api.Helper;
using Control.Api.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SphericalContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("CadenaConexion")));

builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Configuracion.DirectorioLlave()))
    .SetApplicationName("SharedCookie.Spherical");

builder.Services.Configure<CookiePolicyOptions>(options => {
    // This lambda determines whether user consent for non-essential cookies is needed for a given request.
    options.CheckConsentNeeded = context => true;
    options.MinimumSameSitePolicy = SameSiteMode.None;
});

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
    {
        options.Cookie.Name = ".AspNet.SharedCookie.Spherical";
    });

builder.Services.ConfigureApplicationCookie(config => {
    config.Cookie.Name = ".AspNet.SharedCookie.Spherical";
    config.LoginPath = "/Login";
    config.Cookie.Domain = ".localhost";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
