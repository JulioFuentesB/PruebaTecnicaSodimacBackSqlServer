using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoService _productoService;

        public ProductosController(IProductoService productoService)
        {
            _productoService = productoService;
        }



        /// <summary>
        /// Obtiene todos los productos
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductoDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ProductoDto>>> GetProductos()
        {
            try
            {
                var productos = await _productoService.ObtenerTodosAsync();
                return Ok(productos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Obtiene un producto por su ID
        /// </summary>
        /// <param name="id">ID del producto</param>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ProductoDto>> GetProducto(int id)
        {
            try
            {
                var producto = await _productoService.ObtenerPorIdAsync(id);
                return producto == null ? NotFound() : Ok(producto);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Crea un nuevo producto
        /// </summary>
        [HttpPost]
        [ProducesResponseType(typeof(ProductoDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductoDto>> CreateProducto([FromBody] ProductoCreateDto dto)
        {
            try
            {
                var creado = await _productoService.CrearAsync(dto);
                return CreatedAtAction(nameof(GetProducto), new { id = creado.IdProducto }, creado);
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Actualiza un producto existente
        /// </summary>
        /// <param name="id">ID del producto a actualizar</param>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateProducto(int id, [FromBody] ProductoUpdateDto dto)
        {
            try
            {
                await _productoService.ActualizarAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Elimina un producto
        /// </summary>
        /// <param name="id">ID del producto a eliminar</param>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteProducto(int id)
        {
            try
            {
                await _productoService.EliminarAsync(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {

                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }

        /// <summary>
        /// Verifica si un SKU está disponible
        /// </summary>
        /// <param name="sku">SKU a verificar</param>
        /// <param name="idProducto">ID del producto a excluir (opcional)</param>
        [HttpGet("verificar-sku")]
        [ProducesResponseType(typeof(ResultadoValidacionSKU), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ResultadoValidacionSKU>> VerificarSkuUnico(
            [FromQuery] string sku,
            [FromQuery] int? idProducto = null)
        {
            try
            {
                var resultado = await _productoService.VerificarSkuUnicoAsync(sku, idProducto);
                return Ok(resultado);
            }
            catch (Exception ex)
            {

                return StatusCode(500, "Ocurrió un error al procesar la solicitud");
            }
        }



    }


}
