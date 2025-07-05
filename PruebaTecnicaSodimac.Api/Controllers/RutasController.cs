using ClassLibrary1.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public RutasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RutaDto>>> GetRutas()
        {
            return await _context.Ruta
                .Select(r => new RutaDto
                {
                    IdRuta = r.IdRuta,
                    Estado = r.Estado,
                    FechaAsignacion = r.FechaAsignacion,
                    FechaEstimadaEntrega = r.FechaEstimadaEntrega
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RutaDto>> GetRuta(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null) return NotFound();

            return new RutaDto
            {
                IdRuta = ruta.IdRuta,
                Estado = ruta.Estado,
                FechaAsignacion = ruta.FechaAsignacion,
                FechaEstimadaEntrega = ruta.FechaEstimadaEntrega
            };
        }

        [HttpPost]
        public async Task<ActionResult<RutaDto>> CreateRuta(RutaCreateDto dto)
        {
            var ruta = new Ruta
            {
                Estado = "EnTránsito",
                FechaAsignacion = DateTime.UtcNow,
                FechaEstimadaEntrega = dto.FechaEstimadaEntrega
            };

            _context.Ruta.Add(ruta);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetRuta),
                new { id = ruta.IdRuta },
                new RutaDto
                {
                    IdRuta = ruta.IdRuta,
                    Estado = ruta.Estado,
                    FechaAsignacion = ruta.FechaAsignacion,
                    FechaEstimadaEntrega = ruta.FechaEstimadaEntrega
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRuta(int id, RutaUpdateDto dto)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null) return NotFound();

            ruta.Estado = dto.Estado ?? ruta.Estado;
            ruta.FechaEstimadaEntrega = dto.FechaEstimadaEntrega ?? ruta.FechaEstimadaEntrega;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRuta(int id)
        {
            var ruta = await _context.Ruta.FindAsync(id);
            if (ruta == null) return NotFound();

            // Verificar si tiene pedidos asociados
            if (await _context.PedidoRuta.AnyAsync(pr => pr.IdRuta == id))
                return BadRequest("No se puede eliminar una ruta con pedidos asignados");

            _context.Ruta.Remove(ruta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
