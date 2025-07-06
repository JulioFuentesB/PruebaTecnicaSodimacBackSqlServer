using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class RutasController : ControllerBase
    {
        private readonly IRutaService _rutaService;

        public RutasController(IRutaService rutaService)
        {
            _rutaService = rutaService;
        }

        /// <summary>
        /// Obtiene una lista de todas las rutas.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<RutaDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<RutaDto>>> GetRutas()
            => Ok(await _rutaService.ObtenerTodasAsync());

        /// <summary>
        /// Obtiene una ruta por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(RutaDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<RutaDto>> GetRuta(int id)
        {
            var ruta = await _rutaService.ObtenerPorIdAsync(id);
            return ruta == null ? NotFound() : Ok(ruta);
        }

        /// <summary>
        /// Crea una nueva ruta.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(RutaDto), StatusCodes.Status201Created)]
        public async Task<ActionResult<RutaDto>> CreateRuta(RutaCreateDto dto)
        {
            var ruta = await _rutaService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetRuta), new { id = ruta.IdRuta }, ruta);
        }

        /// <summary>
        /// Actualiza una ruta existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateRuta(int id, RutaUpdateDto dto)
        {
            try
            {
                await _rutaService.ActualizarAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Elimina una ruta por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteRuta(int id)
        {
            try
            {
                await _rutaService.EliminarAsync(id);
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
        }
    }

    
}
