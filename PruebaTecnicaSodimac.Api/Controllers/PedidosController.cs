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
        public async Task<IActionResult> GetPedidos(int page = 1, int pageSize = 20) =>
            Ok(await _pedidoService.GetPedidosAsync(page, pageSize));

        /// <summary>
        /// Obtiene un pedido por su ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
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
        public async Task<IActionResult> AsignarRutas([FromBody] List<int> ids)//pedidoIds
        {
            var result = await _pedidoService.AsignarRutasAsync(ids);
            return result == null ? BadRequest("No se pudo asignar rutas.") : Ok(result);
        }

        [HttpGet("pendientes")]
        public async Task<ActionResult<IEnumerable<PedidoPendienteDto>>> GetPedidosPendientes()
        {
            var pedidos = await _pedidoService.ObtenerPedidosPendientesAsync();
            return Ok(pedidos);
        }

    }


    //[ApiController]
    //[Route("api/[controller]")]
    //public class PedidosController : ControllerBase
    //{
    //	private readonly AppDbContext _context;
    //	private readonly IRouteService _routeService;

    //	public PedidosController(AppDbContext context, IRouteService routeService)
    //	{
    //		_context = context;
    //		_routeService = routeService;
    //	}

    //	// CRUD para pedidos ----------------------------------------

    //	[HttpGet]
    //	public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidos(
    //		[FromQuery] int page = 1,
    //		[FromQuery] int pageSize = 20)
    //	{
    //		var pedidos = await _context.Pedidos
    //			.Include(p => p.IdClienteNavigation)
    //			.Include(p => p.PedidoProductos)
    //				.ThenInclude(pp => pp.IdProductoNavigation)
    //			.OrderByDescending(p => p.FechaCreacion)
    //			.Skip((page - 1) * pageSize)
    //			.Take(pageSize)
    //			.Select(p => MapToPedidoDto(p))
    //			.ToListAsync();

    //		return Ok(pedidos);
    //	}

    //	[HttpGet("{id}")]
    //	public async Task<ActionResult<PedidoDto>> GetPedido(int id)
    //	{
    //		var pedido = await _context.Pedidos
    //			.Include(p => p.IdClienteNavigation)
    //			.Include(p => p.PedidoProductos)
    //				.ThenInclude(pp => pp.IdProductoNavigation)
    //			.FirstOrDefaultAsync(p => p.IdPedido == id);

    //		if (pedido == null) return NotFound();

    //		return MapToPedidoDto(pedido);
    //	}

    //	[HttpPost]
    //	public async Task<ActionResult<PedidoDto>> CreatePedido(PedidoCreateDto dto)
    //	{
    //		var cliente = await _context.Clientes.FindAsync(dto.IdCliente);
    //		if (cliente == null) return BadRequest("Cliente no válido");

    //		var pedido = new Pedido
    //		{
    //			IdCliente = dto.IdCliente,
    //			FechaEntrega = dto.FechaEntrega,
    //			Estado = "Pendiente",
    //			PedidoProductos = new List<PedidoProducto>()
    //		};

    //		foreach (var productoDto in dto.Productos)
    //		{
    //			var producto = await _context.Productos.FindAsync(productoDto.IdProducto);
    //			if (producto == null)
    //				return BadRequest($"Producto ID {productoDto.IdProducto} no encontrado");

    //			pedido.PedidoProductos.Add(new PedidoProducto
    //			{
    //				IdProducto = producto.IdProducto,
    //				Cantidad = productoDto.Cantidad
    //			});
    //		}

    //		_context.Pedidos.Add(pedido);
    //		await _context.SaveChangesAsync();

    //		return CreatedAtAction(nameof(GetPedido),
    //			new { id = pedido.IdPedido },
    //			MapToPedidoDto(pedido));
    //	}

    //	[HttpPut("{id}")]
    //	public async Task<IActionResult> UpdatePedido(int id, PedidoUpdateDto dto)
    //	{
    //		var pedido = await _context.Pedidos
    //			.Include(p => p.PedidoProductos)
    //			.FirstOrDefaultAsync(p => p.IdPedido == id);

    //		if (pedido == null) return NotFound();
    //		if (pedido.Estado == "Entregado")
    //			return BadRequest("No se puede modificar un pedido entregado");

    //		// Actualizar propiedades
    //		pedido.FechaEntrega = dto.FechaEntrega ?? pedido.FechaEntrega;

    //		// Actualizar productos
    //		if (dto.Productos != null)
    //		{
    //			pedido.PedidoProductos.Clear();
    //			foreach (var prodDto in dto.Productos)
    //			{
    //				pedido.PedidoProductos.Add(new PedidoProducto
    //				{
    //					IdProducto = prodDto.IdProducto,
    //					Cantidad = prodDto.Cantidad
    //				});
    //			}
    //		}

    //		await _context.SaveChangesAsync();
    //		return NoContent();
    //	}

    //	[HttpDelete("{id}")]
    //	public async Task<IActionResult> DeletePedido(int id)
    //	{
    //		var pedido = await _context.Pedidos.FindAsync(id);
    //		if (pedido == null) return NotFound();
    //		if (pedido.Estado != "Pendiente")
    //			return BadRequest("Solo se pueden eliminar pedidos pendientes");

    //		_context.Pedidos.Remove(pedido);
    //		await _context.SaveChangesAsync();
    //		return NoContent();
    //	}

    //	// Métodos específicos de negocio ----------------------------

    //	[HttpGet("cliente/{IdCliente}")]
    //	public async Task<ActionResult<IEnumerable<PedidoDto>>> GetPedidosPorCliente(int IdCliente)
    //	{
    //		var pedidos = await _context.Pedidos
    //			.Where(p => p.IdCliente == IdCliente)
    //			.Include(p => p.PedidoProductos)
    //				.ThenInclude(pp => pp.IdProductoNavigation)
    //			.Select(p => MapToPedidoDto(p))
    //			.ToListAsync();

    //		return Ok(pedidos);
    //	}

    //	[HttpPost("asignar-rutas")]
    //	public async Task<ActionResult<RouteAssignmentResponse>> AsignarRutas(
    //		[FromBody] List<int> IdPedidos)
    //	{
    //		// Obtener pedidos con información necesaria
    //		var pedidos = await _context.Pedidos
    //			.Where(p => IdPedidos.Contains(p.IdPedido))
    //			.Include(p => p.IdClienteNavigation)
    //			.ToListAsync();

    //		// Validar que todos los pedidos estén pendientes
    //		if (pedidos.Any(p => p.Estado != "Pendiente"))
    //			return BadRequest("Solo se pueden asignar rutas a pedidos pendientes");

    //		// Preparar datos para el SaaS
    //		var request = pedidos.Select(p => new OrderAssignmentRequest
    //		{
    //			IdOrder = p.IdPedido,
    //			DeliveryDate = p.FechaEntrega,
    //			Destination = p.IdClienteNavigation.Direccion
    //		}).ToList();

    //		// Llamar al servicio SaaS
    //		var response = await _routeService.AssignRouteAsync(request);

    //		// Guardar asignaciones en BD
    //		foreach (var assignment in response.Assignments)
    //		{
    //			var pedido = pedidos.First(p => p.IdPedido == assignment.IdOrder);

    //			// Crear nueva ruta
    //			var ruta = new Ruta
    //			{
    //				Estado = "EnTránsito",
    //				FechaAsignacion = DateTime.UtcNow,
    //				FechaEstimadaEntrega = assignment.EstimatedDelivery
    //			};

    //			_context.Ruta.Add(ruta);
    //			await _context.SaveChangesAsync(); // Guardar para obtener ID

    //			// Asociar pedido con ruta
    //			pedido.PedidoRutas.Add(new PedidoRutas
    //			{
    //				IdRuta = ruta.IdRuta,
    //				FechaAsignacion = DateTime.UtcNow
    //			});

    //			// Actualizar estado del pedido
    //			pedido.Estado = "Asignado";
    //		}

    //		await _context.SaveChangesAsync();
    //		return Ok(response);
    //	}

    //	// Helpers --------------------------------------------------

    //	private PedidoDto MapToPedidoDto(Pedido pedido)
    //	{
    //		return new PedidoDto
    //		{
    //			IdPedido = pedido.IdPedido,
    //			Cliente = new ClienteDto
    //			{
    //				IdCliente = pedido.IdClienteNavigation.IdCliente,
    //				Nombre = pedido.IdClienteNavigation.Nombre,
    //				Email = pedido.IdClienteNavigation.Email
    //			},
    //			FechaCreacion = pedido.FechaCreacion ?? DateTime.Now,
    //			FechaEntrega = pedido.FechaEntrega,
    //			Estado = pedido.Estado,
    //			Productos = pedido.PedidoProductos.Select(pp => new ProductoPedidoDto
    //			{
    //				IdProducto = pp.IdProducto,
    //				Nombre = pp.IdProductoNavigation.Nombre,
    //				Cantidad = pp.Cantidad,
    //				Precio = pp.IdProductoNavigation.Precio
    //			}).ToList(),
    //			Rutas = pedido.PedidoRutas.Select(pr => new RutaDto
    //			{
    //				IdRuta = pr.IdRuta,
    //				Estado = pr.IdRutaNavigation.Estado,
    //				FechaAsignacion = pr.FechaAsignacion ?? DateTime.Now //validar
    //			}).ToList()
    //		};
    //	}
    //}
}
