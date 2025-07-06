using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteService _clienteService;

        public ClientesController(IClienteService clienteService)
        {
            _clienteService = clienteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
            => Ok(await _clienteService.ObtenerTodosAsync());

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> GetCliente(int id)
        {
            var cliente = await _clienteService.ObtenerPorIdAsync(id);
            return cliente == null ? NotFound() : Ok(cliente);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> CreateCliente(ClienteCreateDto dto)
        {
            var clienteDto = await _clienteService.CrearAsync(dto);
            return CreatedAtAction(nameof(GetCliente), new { id = clienteDto.IdCliente }, clienteDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCliente(int id, ClienteUpdateDto dto)
        {
            try
            {
                await _clienteService.ActualizarAsync(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCliente(int id)
        {
            try
            {
                await _clienteService.EliminarAsync(id);
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


    //[ApiController]
    //[Route("api/[controller]")]
    //public class ClientesController : ControllerBase
    //{
    //    private readonly IClienteService _clienteService;

    //    public ClientesController(IClienteService clienteService)
    //    {
    //        _clienteService = clienteService;
    //    }

    //    /// <summary>
    //    /// Obtiene una lista de todos los clientes.
    //    /// </summary>
    //    /// <returns></returns>
    //    [HttpGet]
    //    [ProducesResponseType(typeof(IEnumerable<ClienteDto>), StatusCodes.Status200OK)]
    //    public async Task<ActionResult<IEnumerable<ClienteDto>>> GetClientes()
    //        => Ok(await _clienteService.ObtenerTodosAsync());

    //    /// <summary>
    //    /// Obtiene un cliente por su ID.
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    [HttpGet("{id}")]
    //    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status200OK)]
    //    public async Task<ActionResult<ClienteDto>> GetCliente(int id)
    //    {
    //        var cliente = await _clienteService.ObtenerPorIdAsync(id);
    //        return cliente == null ? NotFound() : Ok(cliente);
    //    }

    //    /// <summary>
    //    /// Crea un nuevo cliente.
    //    /// </summary>
    //    /// <param name="dto"></param>
    //    /// <returns></returns>
    //    [HttpPost]
    //    [ProducesResponseType(typeof(ClienteDto), StatusCodes.Status201Created)]
    //    public async Task<ActionResult<ClienteDto>> CreateCliente(ClienteCreateDto dto)
    //    {
    //        var clienteDto = await _clienteService.CrearAsync(dto);
    //        return CreatedAtAction(nameof(GetCliente), new { id = clienteDto.IdCliente }, clienteDto);
    //    }

    //    /// <summary>
    //    /// Actualiza un cliente existente.
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <param name="dto"></param>
    //    /// <returns></returns>
    //    [HttpPut("{id}")]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    public async Task<IActionResult> UpdateCliente(int id, ClienteUpdateDto dto)
    //    {
    //        try
    //        {
    //            await _clienteService.ActualizarAsync(id, dto);
    //            return NoContent();
    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            return NotFound();
    //        }
    //    }

    //    /// <summary>
    //    /// Elimina un cliente por su ID.
    //    /// </summary>
    //    /// <param name="id"></param>
    //    /// <returns></returns>
    //    [HttpDelete("{id}")]
    //    [ProducesResponseType(StatusCodes.Status204NoContent)]
    //    public async Task<IActionResult> DeleteCliente(int id)
    //    {
    //        try
    //        {
    //            await _clienteService.EliminarAsync(id);
    //            return NoContent();
    //        }
    //        catch (KeyNotFoundException)
    //        {
    //            return NotFound();
    //        }
    //        catch (InvalidOperationException ex)
    //        {
    //            return BadRequest(ex.Message);
    //        }
    //    }
    //}


}
