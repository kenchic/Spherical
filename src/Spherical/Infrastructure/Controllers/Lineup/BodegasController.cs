using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Infrastructure.EF.Models;
using Spherical.Client.DTO.Lineup;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Lineup
{
    [Authorize]
    [Route("api/v1/bodegas")]
    [ApiController]
    public class BodegasController : ControllerBase
    {
        private readonly SphericalContext _context;

        public BodegasController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/bodegas
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<BodegaDto>>>> GetBodegas()
        {
            try
            {
                var bodegas = await _context.Bodegas
                    .Where(b => b.Activo)
                    .OrderBy(b => b.Nombre)
                    .Select(b => EntityToDTO(b))
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<BodegaDto>>
                {
                    Data = bodegas,
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        // GET: api/v1/bodegas/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<BodegaDto>>> GetBodega(int id)
        {
            try
            {
                var b = await _context.Bodegas.FirstOrDefaultAsync(x => x.Id == id);
                if (b == null)
                {
                    var notFound = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "Bodega no encontrada");
                    return NotFound(notFound);
                }

                var response = new ApiResponse<BodegaDto>
                {
                    Data = EntityToDTO(b),
                    Success = true
                };
                return Ok(response);
            }
            catch (Exception ex)
            {
                var response = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(response);
            }
        }

        private static BodegaDto EntityToDTO(Bodega bodega) => new BodegaDto
        {
            Id = bodega.Id,
            IdProyecto = bodega.IdProyecto,
            IdProveedor = bodega.IdProveedor,
            Empresa = bodega.Empresa,
            Codigo = bodega.Codigo,
            Nombre = bodega.Nombre,
            EsSistema = bodega.EsSistema,
            Activo = bodega.Activo
        };
    }
}
