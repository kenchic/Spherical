using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Defender.Api.Models;
using Creative.DTO.Defender;
using Creative.DTO.Spherical;
using System.Net;

namespace Defender.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly SphericalContext _context;

        public UsuariosController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/Usuarios
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<UsuarioDTO>>>> GetUsuarios()
        {

            if (_context.Usuarios == null)
            {
                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                return NotFound(res);
            }

            var listUsuarios = await _context.Usuarios.Select(x => EntityToDTO(x)).ToListAsync();

            if (listUsuarios.Count < 0)
            {
                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                return NotFound();
            }

            try
            {
                var res = new ApiResponse<IEnumerable<UsuarioDTO>>();
                var listMenu = await _context.Usuarios.Select(x => EntityToDTO(x)).ToListAsync();
                res.Data = listMenu;
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }

        }

        // GET: api/Usuarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> GetUsuario(string id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);

            if (usuario == null)
            {
                return NotFound();
            }

            var res = new ApiResponse<UsuarioDTO>()
            {
                Data = EntityToDTO(usuario)
            };

            return Ok(res);
        }

        // PUT: api/Usuarios/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(string id, UsuarioDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(model => model.Id == id);
            if (usuario == null)
            {
                return NotFound();
            }

            usuario.Id = dto.Id;
            usuario.Nombre = dto.Nombre;

            _context.Entry(dto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!UsuarioExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return NoContent();
        }

        // POST: api/Usuarios
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<UsuarioDTO>>> PostUsuario(UsuarioDTO dto)
        {
            if (_context.Usuarios == null)
            {
                return Problem("Entity set 'SphericalContext.Usuarios'  is null.");
            }

            var usuario = new Usuario()
            {
                Id = dto.Id,
                Nombre = dto.Nombre
            };

            _context.Usuarios.Add(usuario);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (UsuarioExists(dto.Id))
                {
                    return Conflict();
                }
                else
                {
                    return BadRequest(ex.Message);
                }
            }

            return CreatedAtAction("GetUsuario", new { id = dto.Id }, dto);
        }

        // DELETE: api/Usuarios/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(string id)
        {
            if (_context.Usuarios == null)
            {
                return NotFound();
            }
            var usuario = await _context.Usuarios.FindAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UsuarioExists(string id)
        {
            return (_context.Usuarios?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static UsuarioDTO EntityToDTO(Usuario usuario) =>
        new UsuarioDTO
        {
            Id = usuario.Id,
            Nombre = usuario.Nombre,
        };
    }
}
