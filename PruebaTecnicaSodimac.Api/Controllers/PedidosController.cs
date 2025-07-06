// <copyright file="PedidosController.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Microsoft.AspNetCore.Mvc;

using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar pedidos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PedidosController : ControllerBase
    {
        private readonly IPedidoService _pedidoService;

        /// <summary>
        /// Constructor del controlador PedidosController.
        /// </summary>
        /// <param name="pedidoService"></param>
        public PedidosController(IPedidoService pedidoService)
        {
            _pedidoService = pedidoService;
        }

        /// <summary>
        /// Obtiene una lista paginada de pedidos.
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PedidoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPedidos(int page = 1, int pageSize = 20) =>
            Ok(await _pedidoService.GetPedidosAsync(page, pageSize));

        /// <summary>
        /// Obtiene un pedido por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PedidoDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPedido(int id)
        {
            try
            {
                var pedido = await _pedidoService.GetPedidoAsync(id);
                return pedido == null ? NotFound() : Ok(pedido);
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// Crea un nuevo pedido.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(typeof(PedidoDto), StatusCodes.Status201Created)]
        public async Task<IActionResult> CreatePedido(PedidoCreateDto dto)
        {
            var pedido = await _pedidoService.CreatePedidoAsync(dto);
            return pedido == null
                ? BadRequest("Error al crear el pedido.")
                : CreatedAtAction(nameof(GetPedido), new { id = pedido.IdPedido }, pedido);
        }

        /// <summary>
        /// Actualiza un pedido existente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdatePedido(int id, PedidoUpdateDto dto)
        {
            var result = await _pedidoService.UpdatePedidoAsync(id, dto);
            return result ? NoContent() : BadRequest("No se pudo actualizar el pedido.");
        }

        /// <summary>
        /// Elimina un pedido por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletePedido(int id)
        {
            var result = await _pedidoService.DeletePedidoAsync(id);
            return result ? NoContent() : BadRequest("No se pudo eliminar el pedido.");
        }

        /// <summary>
        /// Obtiene los pedidos de un cliente específico.
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        [HttpGet("cliente/{idCliente}")]
        [ProducesResponseType(typeof(IEnumerable<PedidoDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPedidosPorCliente(int idCliente) =>
            Ok(await _pedidoService.GetPedidosPorClienteAsync(idCliente));

        /// <summary>
        /// Asigna rutas a una lista de pedidos.
        ///  Notificación al SaaS para asignar rutas a pedidos
        ///  asigna pedidos  unn ente externo saas
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost("asignar-rutas")]
        [ProducesResponseType(typeof(RouteAssignmentResponse), StatusCodes.Status200OK)]
        public async Task<IActionResult> AsignarRutas([FromBody] List<int> ids)//pedidoIds
        {
            var result = await _pedidoService.AsignarRutasAsync(ids);
            return result == null ? BadRequest("No se pudo asignar rutas.") : Ok(result);
        }

        /// <summary>
        /// Obtiene una lista de pedidos pendientes.
        /// </summary>
        /// <returns></returns>
        [HttpGet("pendientes")]
        [ProducesResponseType(typeof(IEnumerable<PedidoPendienteDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<PedidoPendienteDto>>> GetPedidosPendientes()
        {
            var pedidos = await _pedidoService.ObtenerPedidosPendientesAsync();
            return Ok(pedidos);
        }

    }



}
