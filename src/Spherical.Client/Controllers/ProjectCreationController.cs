using Creative.Modelos.Lineup;
using Spherical.Client.DTO.Common;
using Spherical.Client.DTO.Spherical;
using Spherical.Client.Services;

namespace Spherical.Client.Controllers
{
    public class ProjectCreationController
    {
        private readonly IClientService _clientService;
        private readonly ICatalogService _catalogService;
        private readonly IProjectService _projectService;

        public ProjectCreationController(IClientService clientService, ICatalogService catalogService, IProjectService projectService)
        {
            _clientService = clientService;
            _catalogService = catalogService;
            _projectService = projectService;
        }

        public async Task<(bool ok, string? error, ClienteModeloDto? creado)> CrearClienteAsync(ClienteModeloDto cliente)
        {
            var e = ValidarCliente(cliente);
            if (!string.IsNullOrEmpty(e)) return (false, e, null);
            var resp = await _clientService.CreateClienteAsync(cliente);
            return (resp.Success && resp.Data != null, resp.ErrorMessage, resp.Data);
        }

        public async Task<(bool ok, string? error, CatalogoDetalleModelo? creado)> CrearCiudadAsync(string idCatalogo, CatalogoDetalleModelo detalle)
        {
            var e = ValidarCatalogoDetalle(detalle);
            if (!string.IsNullOrEmpty(e)) return (false, e, null);
            var resp = await _catalogService.CreateDetalleAsync(idCatalogo, detalle);
            return (resp.Success && resp.Data != null, resp.ErrorMessage, resp.Data);
        }

        public async Task<(bool ok, string? error, ProyectoModeloDto? creado)> CrearProyectoAsync(ProyectoModeloDto proyecto)
        {
            var e = ValidarProyecto(proyecto);
            if (!string.IsNullOrEmpty(e)) return (false, e, null);
            var resp = await _projectService.CreateProyectoAsync(proyecto);
            return (resp.Success && resp.Data != null, resp.ErrorMessage, resp.Data);
        }

        public string? ValidarCliente(ClienteModeloDto c)
        {
            if (string.IsNullOrWhiteSpace(c.Identificacion) || string.IsNullOrWhiteSpace(c.Nombre1) || string.IsNullOrWhiteSpace(c.Apellido1) || string.IsNullOrWhiteSpace(c.Direccion) || string.IsNullOrWhiteSpace(c.Telefono) || string.IsNullOrWhiteSpace(c.IdCiudad))
                return "Campos obligatorios de cliente faltantes";
            return null;
        }

        public string? ValidarProyecto(ProyectoModeloDto p)
        {
            if (string.IsNullOrWhiteSpace(p.Nombre) || string.IsNullOrWhiteSpace(p.Tipo) || p.idCliente <= 0 || string.IsNullOrWhiteSpace(p.idCiudad))
                return "Campos obligatorios de proyecto faltantes";
            if (!string.IsNullOrWhiteSpace(p.idCiudad) && p.idCiudad.Length > 20) return "Código de ciudad inválido";
            return null;
        }

        public string? ValidarCatalogoDetalle(CatalogoDetalleModelo d)
        {
            if (string.IsNullOrWhiteSpace(d.Id) || string.IsNullOrWhiteSpace(d.Nombre)) return "Detalle de catálogo incompleto";
            return null;
        }
    }
}
