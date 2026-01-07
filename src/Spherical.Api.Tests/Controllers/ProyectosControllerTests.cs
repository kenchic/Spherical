using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Creative.Modelos.Lineup;
using Spherical.Api.Tests.Helpers;
using Spherical.Client.DTO.Spherical;
using Xunit;

namespace Spherical.Api.Tests.Controllers
{
    public class ProyectosControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;
        public ProyectosControllerTests(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CrearProyecto_Exito()
        {
            var client = _factory.CreateClient();

            var nuevoCliente = new ClienteModeloDto
            {
                IdCiudad = "CALI",
                Identificacion = "800123",
                Nombre1 = "Cliente",
                Apellido1 = "SAS",
                Direccion = "Av 3",
                Telefono = "333333",
                Activo = true
            };
            var respCli = await client.PostAsJsonAsync("api/v1/clientes", nuevoCliente);
            var creadoCli = JsonSerializer.Deserialize<ApiResponse<ClienteModeloDto>>(await respCli.Content.ReadAsStringAsync());
            var idCliente = creadoCli!.Data!.Id;

            var proyecto = new ProyectoModeloDto
            {
                IdCliente = idCliente,
                IdCiudad = "CALI",
                Nombre = "Edificio Central",
                Tipo = "Construcción",
                Direccion = "Av 3",
                Telefono = "333333",
                Observacion = "",
                Fecha = DateTime.Today,
                FormaContacto = "Teléfono",
                SistemaMedida = "Pulgadas",
                Activo = true
            };

            var resp = await client.PostAsJsonAsync("api/v1/proyectos", proyecto);
            Assert.True(resp.IsSuccessStatusCode);
            var creado = JsonSerializer.Deserialize<ApiResponse<ProyectoModeloDto>>(await resp.Content.ReadAsStringAsync());
            Assert.NotNull(creado);
            Assert.True(creado!.Success);
            Assert.True(creado!.Data!.Id > 0);
        }
    }
}
