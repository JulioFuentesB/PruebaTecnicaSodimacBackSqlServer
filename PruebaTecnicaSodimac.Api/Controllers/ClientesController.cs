using ClassLibrary1.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ClientesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
        {
            return await _context.Clientes
                .Select(c => new ClienteDto
                {
                    IdCliente = c.IdCliente,
                    Nombre = c.Nombre,
                    Email = c.Email,
                    Direccion = c.Direccion
                })
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Direccion = cliente.Direccion
            };
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> CreateCliente(ClienteCreateDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCliente),
                new { id = cliente.IdCliente },
                new ClienteDto
                {
                    IdCliente = cliente.IdCliente,
                    Nombre = cliente.Nombre,
                    Email = cliente.Email,
                    Direccion = cliente.Direccion
                });
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteUpdateDto dto)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            cliente.Nombre = dto.Nombre ?? cliente.Nombre;
            cliente.Direccion = dto.Direccion ?? cliente.Direccion;
            cliente.Email = dto.Email ?? cliente.Email;
            cliente.Telefono = dto.Telefono ?? cliente.Telefono;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null) return NotFound();

            // Verificar si tiene pedidos asociados
            if (await _context.Pedidos.AnyAsync(p => p.IdCliente == id))
                return BadRequest("No se puede eliminar un cliente con pedidos asociados");

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
