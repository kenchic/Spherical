using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Creative.Modelos.Lineup;
using Spherical.Api.Tests.Helpers;
using Spherical.Client.DTO.Spherical;
using Xunit;

namespace Spherical.Api.Tests.Controllers
{
    public class ClientesControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;
        public ClientesControllerTests(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task CrearYObtenerCliente_Exito()
        {
            var client = _factory.CreateClient();
            var nuevo = new ClienteModeloDto
            {
                idCiudad = "BOG",
                Identificacion = "900123",
                Nombre1 = "Empresa",
                Apellido1 = "SAS",
                Direccion = "Calle 1",
                Telefono = "111111",
                Activo = true
            };

            var respCrear = await client.PostAsJsonAsync("api/v1/clientes", nuevo);
            Assert.True(respCrear.IsSuccessStatusCode);
            var creado = JsonSerializer.Deserialize<ApiResponse<ClienteModeloDto>>(await respCrear.Content.ReadAsStringAsync());
            Assert.NotNull(creado);
            Assert.True(creado!.Success);
            var id = creado!.Data!.Id;

            var respGet = await client.GetAsync($"api/v1/clientes/{id}");
            Assert.True(respGet.IsSuccessStatusCode);
            var cli = JsonSerializer.Deserialize<ApiResponse<VClienteModeloDto>>(await respGet.Content.ReadAsStringAsync());
            Assert.NotNull(cli);
            Assert.Equal("900123", cli!.Data!.Identificacion);
        }

        [Fact]
        public async Task CrearCliente_Duplicado_Conflict()
        {
            var client = _factory.CreateClient();
            var c = new ClienteModeloDto
            {
                idCiudad = "MED",
                Identificacion = "123",
                Nombre1 = "ACME",
                Apellido1 = "LTDA",
                Direccion = "Calle 2",
                Telefono = "222222",
                Activo = true
            };
            var r1 = await client.PostAsJsonAsync("api/v1/clientes", c);
            Assert.True(r1.IsSuccessStatusCode);

            var r2 = await client.PostAsJsonAsync("api/v1/clientes", c);
            Assert.Equal(HttpStatusCode.Conflict, r2.StatusCode);
        }
    }
}
