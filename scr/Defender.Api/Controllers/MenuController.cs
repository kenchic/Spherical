using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Creative.DTO.Lineup;
using Defender.Api.Models;
using Microsoft.Data.SqlClient;
using Creative.DTO.Defender;
using Newtonsoft.Json.Linq;

namespace Defender.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly SphericalContext _context;

        public MenuController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/Menu
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuDTO>>> GetMenus()
        {
            var listMenu = await _context.Menus.Select(x => EntityToDTO(x)).ToListAsync();
            return listMenu;
        }

        // GET: api/Menu/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MenuDTO>> GetMenu(string id)
        {

            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            return EntityToDTO(menu);
        }

        // GET: api/Usuarios/MenuUsuario
        [HttpGet("{usuario}/{empresa}/{sistema}")]
        public async Task<ActionResult<IEnumerable<MenuUsuarioDTO>>> MenuUsuario(string usuario, string empresa, string sistema)
        {
            var listMenu = await _context.MenuUsuario.FromSqlRaw($"Defender.GetMenuUsuario {usuario}, {empresa}, {sistema}").ToListAsync();
            return listMenu;
        }

        // PUT: api/Menu/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMenu(string id, MenuDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var menu = await _context.Menus.FirstOrDefaultAsync(model => model.Id == id);
            if (menu == null)
            {
                return NotFound();
            }

            menu.Id = dto.Id;
            menu.Empresa = dto.Empresa;
            menu.IdMenu = dto.IdMenu;
            menu.Nombre = dto.Nombre;
            menu.Url = dto.Url;
            menu.Orden = dto.Orden;
            menu.Icono = dto.Icono;

            _context.Entry(menu).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MenuExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Menu
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MenuDTO>> PostMenu(MenuDTO dto)
        {
            if (_context.Menus == null)
            {
                return Problem("Entity set 'SphericalContext.Menus'  is null.");
            }
            var menu = new Menu()
            {
                Id = dto.Id,
                Empresa = dto.Empresa,
                IdMenu = dto.IdMenu,
                Nombre = dto.Nombre,
                Url = dto.Url,
                Orden = dto.Orden,
                Icono = dto.Icono
            };

            _context.Menus.Add(menu);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (MenuExists(menu.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetMenu", new { id = menu.Id }, menu);
        }

        // DELETE: api/Menu/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMenu(string id)
        {
            var menu = await _context.Menus.FindAsync(id);
            if (menu == null)
            {
                return NotFound();
            }

            _context.Menus.Remove(menu);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MenuExists(string id)
        {
            return _context.Menus.Any(e => e.Id == id);
        }

        private static MenuDTO EntityToDTO(Menu menu) =>
        new MenuDTO
        {
            Id = menu.Id,
            Empresa = menu.Empresa,
            IdMenu = menu.IdMenu,
            Nombre = menu.Nombre,
            Url = menu.Url,
            Orden = menu.Orden,
            Icono = menu.Icono
        };
    }
}