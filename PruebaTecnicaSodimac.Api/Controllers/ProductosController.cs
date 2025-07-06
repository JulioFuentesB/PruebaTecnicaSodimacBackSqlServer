using ClassLibrary1.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProductosController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene una lista de todos los productos.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            return await _context.Productos
                .Select(p => new ProductoDto
                {
                    IdProducto = p.IdProducto,
                    Nombre = p.Nombre,
                    Sku = p.Sku,
                    Precio = p.Precio
                })
                .ToListAsync();
        }

        /// <summary>
        /// Obtiene un producto por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Sku = producto.Sku,
                Precio = producto.Precio
            };
        }



        /// <summary>
        /// Crea un nuevo producto.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<ProductoDto>> CreateProducto(ProductoCreateDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Sku = dto.Sku,
                Precio = dto.Precio
            };

            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetProducto),
                new { id = producto.IdProducto },
                new ProductoDto
                {
                    IdProducto = producto.IdProducto,
                    Nombre = producto.Nombre,
                    Sku = producto.Sku,
                    Precio = producto.Precio
                });
        }

        /// <summary>
        /// Actualiza un producto existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProducto(int id, ProductoUpdateDto dto)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            producto.Nombre = dto.Nombre ?? producto.Nombre;
            producto.Sku = dto.Sku ?? producto.Sku;
            producto.Precio = dto.Precio ?? producto.Precio;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Elimina un producto por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null) return NotFound();

            // Verificar si el producto está en pedidos
            if (await _context.PedidoProductos.AnyAsync(pp => pp.IdProducto == id))
                return BadRequest("No se puede eliminar un producto que está en pedidos");

            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }


}
