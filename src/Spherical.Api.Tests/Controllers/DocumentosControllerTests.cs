using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using Spherical.Api.Models;
using Spherical.Api.Tests.Helpers;
using Spherical.Client.DTO.Inventory;
using System.Net;
using System.Net.Http.Json;
using Xunit;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using Spherical.Client.DTO.Spherical;

namespace Spherical.Api.Tests.Controllers
{
    public class DocumentosControllerTests : IClassFixture<ApiWebApplicationFactory>
    {
        private readonly ApiWebApplicationFactory _factory;

        public DocumentosControllerTests(ApiWebApplicationFactory factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task ActualizarDocumento_Exito()
        {
            var client = _factory.CreateClient();

            var crear = new DocumentoDto
            {
                IdDocumentoTipo = "MV",
                IdBodegaOrigen = 1,
                IdBodegaDestino = 2,
                Empresa = "ACME",
                Numero = 100,
                Fecha = DateTime.UtcNow,
                Descripcion = "Inicial",
                Estado = "ABIERTO",
                Detalles = new List<DocumentoDetalleDto>
                {
                    new DocumentoDetalleDto{ IdElemento = 1, Cantidad = 3 },
                    new DocumentoDetalleDto{ IdElemento = 2, Cantidad = 5 }
                }
            };

            var respCrear = await client.PostAsJsonAsync("api/v1/documentos", crear);
            Assert.True(respCrear.IsSuccessStatusCode);
            var creado = JsonSerializer.Deserialize<ApiResponse<DocumentoDto>>(await respCrear.Content.ReadAsStringAsync());
            Assert.NotNull(creado);
            Assert.True(creado!.Success);
            var id = creado.Data!.Id;

            var actualizar = new DocumentoDto
            {
                Id = id,
                IdDocumentoTipo = "MV",
                IdBodegaOrigen = 3,
                IdBodegaDestino = 4,
                Empresa = "ACME",
                Numero = 101,
                Fecha = DateTime.UtcNow,
                Descripcion = "Actualizado",
                Estado = "ABIERTO",
                Detalles = new List<DocumentoDetalleDto>
                {
                    new DocumentoDetalleDto{ IdElemento = 3, Cantidad = 2 }
                }
            };

            var respActualizar = await client.PutAsJsonAsync($"api/v1/documentos/{id}", actualizar);
            Assert.True(respActualizar.IsSuccessStatusCode);
            var ok = JsonSerializer.Deserialize<ApiResponse<bool>>(await respActualizar.Content.ReadAsStringAsync());
            Assert.NotNull(ok);
            Assert.True(ok!.Success);
            Assert.True(ok.Data);

            var respGet = await client.GetAsync($"api/v1/documentos/{id}");
            Assert.True(respGet.IsSuccessStatusCode);
            var doc = JsonSerializer.Deserialize<ApiResponse<DocumentoDto>>(await respGet.Content.ReadAsStringAsync());
            Assert.NotNull(doc);
            Assert.Equal("Actualizado", doc!.Data!.Descripcion);
            Assert.Single(doc.Data.Detalles);
            Assert.Equal(3, doc.Data.Detalles[0].IdElemento);
        }

        [Fact]
        public async Task ActualizarDocumento_NoExiste_404()
        {
            var client = _factory.CreateClient();
            var dto = new DocumentoDto
            {
                IdDocumentoTipo = "MV",
                IdBodegaOrigen = 1,
                IdBodegaDestino = 2,
                Empresa = "ACME",
                Numero = 100,
                Fecha = DateTime.UtcNow,
                Descripcion = "Prueba",
                Estado = "ABIERTO",
                Detalles = new List<DocumentoDetalleDto>()
            };

            var resp = await client.PutAsJsonAsync("api/v1/documentos/999999", dto);
            Assert.Equal(HttpStatusCode.NotFound, resp.StatusCode);
        }

        [Fact]
        public async Task ActualizarDocumento_BadRequest_400()
        {
            var client = _factory.CreateClient();
            var dto = new DocumentoDto
            {
                // IdDocumentoTipo faltante para provocar ModelState error
                IdBodegaOrigen = 1,
                IdBodegaDestino = 2,
                Empresa = "ACME",
                Numero = 100,
                Fecha = DateTime.UtcNow,
                Descripcion = "Prueba",
                Estado = "ABIERTO",
                Detalles = new List<DocumentoDetalleDto>()
            };

            var resp = await client.PutAsJsonAsync("api/v1/documentos/1", dto);
            Assert.Equal(HttpStatusCode.BadRequest, resp.StatusCode);
        }
    }
}
