using Bunit;
using Microsoft.Extensions.DependencyInjection;
using MudBlazor.Services;
using Spherical.Client.Services;
using Spherical.Client.DTO.Spherical;
using Creative.Modelos.Lineup;
using Spherical.Blazor.Components;
using Xunit;

namespace Spherical.Blazor.Tests
{
    public class ProjectCreationComponentTests
    {
        [Fact]
        public void Render_ProjectCreation_ShouldShowTitulo()
        {
            using var ctx = new TestContext();
            ctx.Services.AddMudServices();
            ctx.Services.AddScoped<IClientService>(_ => new FakeClientService());
            ctx.Services.AddScoped<IProjectService>(_ => new FakeProjectService());
            var cut = ctx.RenderComponent<ProjectCreation>();
            Assert.Contains("Nuevo Proyecto", cut.Markup);
        }
    }

    internal class FakeClientService : IClientService
    {
        public void SetAuth(string token) { }
        public Task<ApiResponse<ClienteModelo>> CreateClienteAsync(ClienteModelo cliente) => Task.FromResult(new ApiResponse<ClienteModelo> { Success = true, Data = cliente });
        public Task<ApiResponse<VClienteModelo>> GetClienteAsync(int id) => Task.FromResult(new ApiResponse<VClienteModelo> { Success = true, Data = new VClienteModelo { Id = id, Nombre = "Test" } });
        public Task<ApiResponse<IEnumerable<VClienteModelo>>> GetClientesAsync() => Task.FromResult(new ApiResponse<IEnumerable<VClienteModelo>> { Success = true, Data = Enumerable.Empty<VClienteModelo>() });
    }

    internal class FakeProjectService : IProjectService
    {
        public void SetAuth(string token) { }
        public Task<ApiResponse<ProyectoModelo>> CreateProyectoAsync(ProyectoModelo proyecto) => Task.FromResult(new ApiResponse<ProyectoModelo> { Success = true, Data = proyecto });
        public Task<ApiResponse<VProyectoModelo>> GetProyectoAsync(int id) => Task.FromResult(new ApiResponse<VProyectoModelo> { Success = true, Data = new VProyectoModelo { Id = id, Nombre = "P" } });
        public Task<ApiResponse<IEnumerable<VProyectoModelo>>> GetProyectosAsync() => Task.FromResult(new ApiResponse<IEnumerable<VProyectoModelo>> { Success = true, Data = Enumerable.Empty<VProyectoModelo>() });
    }
}
