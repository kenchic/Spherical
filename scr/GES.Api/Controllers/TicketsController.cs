using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GES.Api.Models;
using CoreGeneral.DTO.GES;

namespace GES.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly SAFContext _context;

        public TicketsController(SAFContext context)
        {
            _context = context;
        }

        // GET: api/Tickets
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TicketDTO>>> GetTickets()
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }

            var listTikets = await _context.Tickets.Select(x => EntityToDTO(x)).ToListAsync();
            return listTikets;
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TicketDTO>> GetTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            return EntityToDTO(ticket);
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, TicketDTO dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var ticket = await _context.Tickets.FirstOrDefaultAsync(model => model.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            ticket.Titulo = dto.Titulo;
            ticket.Descripcion = dto.Descripcion;
            ticket.Tipo = dto.Tipo;
            ticket.Prioridad = dto.Prioridad;
            ticket.Estado = dto.Estado;

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
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

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TicketDTO>> PostTicket(TicketDTO dto)
        {
            if (_context.Tickets == null)
            {
                return Problem("Entity set 'SAFContext.Tickets'  is null.");
            }

            var ticket = new Ticket()
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                Tipo = dto.Tipo,
                Prioridad = dto.Prioridad,
                Estado = dto.Estado,
                FechaCreacion = DateTime.Now
            };

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            if (_context.Tickets == null)
            {
                return NotFound();
            }
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return (_context.Tickets?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private static TicketDTO EntityToDTO(Ticket ticket) =>
        new TicketDTO
        {
            Id = ticket.Id,    
            Titulo = ticket.Titulo,
            Descripcion = ticket.Descripcion,
            Tipo = ticket.Tipo,
            Prioridad = ticket.Prioridad,            
            Estado = ticket.Estado,
            FechaCreacion = ticket.FechaCreacion
        };
    }
}
