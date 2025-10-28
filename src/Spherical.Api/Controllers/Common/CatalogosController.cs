using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Common;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Spherical.Api.Controllers.Common
{
    [Route("api/v1/catalogos")]
    [ApiController]
    public class CatalogosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public CatalogosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/v1/catalogos/{id}/detalles
        [HttpGet("{id}/detalles")]
        public async Task<ActionResult<ApiResponse<IEnumerable<CatalogoDetalleModelo>>>> GetDetalles(string id)
        {
            try
            {
                var detalles = await _context.CatalogoDetalles
                    .Where(cd => cd.IdCatalogo == id && (cd.Activo ?? true))
                    .OrderBy(cd => cd.Nombre)
                    .Select(cd => new CatalogoDetalleModelo
                    {
                        Id = cd.Id,
                        Nombre = cd.Nombre,
                        ValorCadena = cd.ValorCadena,
                        ValorNumero = cd.ValorNumero,
                        ValorDecimal = cd.ValorDecimal,
                        Activo = cd.Activo
                    })
                    .ToListAsync();

                var response = new ApiResponse<IEnumerable<CatalogoDetalleModelo>>
                {
                    Data = detalles,
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
    }
}