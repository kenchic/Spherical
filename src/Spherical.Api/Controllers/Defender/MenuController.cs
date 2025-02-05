using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Spherical.Api.Models;
using Spherical.Client.DTO.Defender;
using Spherical.Client.DTO.Spherical;
using System.Net;

namespace Defender.Api.Controllers
{
    [Authorize]
    [Route("api/v1/menu")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly SphericalContext _context;

        public MenuController(SphericalContext context)
        {
            _context = context;
        }

        // GET: api/Menus
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<MenuDTO>>>> Menus()
        {
            try
            {
                if (_context.Menus == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                    return NotFound(res);
                }
                else
                {
                    var listMenu = await _context.Menus.Select(x => EntityToDTO(x)).ToListAsync();
                    if (listMenu.Count > 0)
                    {
                        var res = new ApiResponse<IEnumerable<MenuDTO>>();
                        res.Data = listMenu;
                        return Ok(res);
                    }
                    else
                    {
                        var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty, "No se encontraron datos");
                        return NotFound(res);
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }
        }

        // GET: api/UserMenuApp/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<MenuDTO>>> Menu(string id)
        {
            try
            {
                var menu = await _context.Menus.FindAsync(id);
                if (menu == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                    return NotFound(res);
                }
                else
                {
                    var res = new ApiResponse<MenuDTO>();
                    res.Data = EntityToDTO(menu);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }
        }

        // GET: api/Empresa/Usuario/UserMenuApp
        [HttpGet("{user}/{company}")]
        public async Task<ActionResult<ApiResponse<IEnumerable<UserMenuDTO>>>> Menu(string user, string company)
        {
            try
            {
                var res = new ApiResponse<IEnumerable<UserMenuDTO>>();
                var listMenu = await _context.UserMenuDTO.FromSqlRaw(
                              $"EXEC Spherical.GetMenuUsuario @p0, @p1", 
                              user.ToUpper(),company.ToUpper())
                    .ToListAsync();
                res.Data = listMenu;
                return Ok(res);
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }
        }

        // PUT: api/UserMenuApp/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> Menu(string id, MenuDTO dto)
        {
            try
            {
                if (id != dto.Id)
                {
                    var res = new ApiResponse<string>();
                    res.Success = false;
                    res.ErrorMessage = "Id diferente fuente datos";
                    return BadRequest(res);
                }
                else
                {
                    var menu = await _context.Menus.FirstOrDefaultAsync(model => model.Id == id);
                    if (menu == null)
                    {
                        var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                        return NotFound(res);
                    }
                    else
                    {
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
                            var res = new ApiResponse<string>();
                            await _context.SaveChangesAsync();
                            return Ok(res);
                        }
                        catch (DbUpdateConcurrencyException ex)
                        {
                            if (!MenuExists(id))
                            {
                                var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
                                return NotFound(res);
                            }
                            else
                            {
                                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                                return BadRequest(res);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }
        }

        // POST: api/UserMenuApp
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ApiResponse<MenuDTO>>> Menu(MenuDTO dto)
        {
            try
            {
                if (_context.Menus == null)
                {
                    var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "El conjunto de entidades 'SphericalContext.Menus' es nulo.");
                    return BadRequest(res);
                }
                else
                {
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
                        var res = new ApiResponse<MenuDTO>();
                        res.Data = new MenuDTO()
                        {
                            Id = menu.Id
                        };
                        return Ok(res);
                    }
                    catch (DbUpdateException ex)
                    {
                        if (MenuExists(menu.Id))
                        {
                            var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, "El identificador ya existe");
                            return BadRequest(res);
                        }
                        else
                        {
                            var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                            return BadRequest(res);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
                return BadRequest(res);
            }
        }

        //// DELETE: api/UserMenuApp/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> UserMenuApp(string id)
        //{
        //    try
        //    {
        //        var menu = await _context.Menus.FindAsync(id);
        //        if (menu == null)
        //        {
        //            var res = new ApiResponse<string>(HttpStatusCode.NotFound, string.Empty);
        //            return NotFound(res);
        //        }
        //        else
        //        {
        //            _context.Menus.Remove(menu);
        //            await _context.SaveChangesAsync();
        //            return Ok();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        var res = new ApiResponse<string>(HttpStatusCode.BadRequest, string.Empty, ex.Message);
        //        return BadRequest(res);
        //    }
        //}

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