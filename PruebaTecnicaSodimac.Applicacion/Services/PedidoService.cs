// <copyright file="PedidoService.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using ClassLibrary1.Data.Entities;

using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services
{
    public class PedidoService : IPedidoService
    {
        private readonly IPedidoRepository _repository;
        private readonly IRouteService _routeService;

        public PedidoService(IPedidoRepository repository, IRouteService routeService)
        {
            _repository = repository;
            _routeService = routeService;
        }

        public async Task<IEnumerable<PedidoDto>> GetPedidosAsync(int page, int pageSize)
        {
            var pedidos = await _repository.GetPedidosAsync(page, pageSize);
            return pedidos.Select(MapToPedidoDto);
        }

        public async Task<PedidoDto?> GetPedidoAsync(int id)
        {
            var pedido = await _repository.GetPedidoByIdAsync(id);
            return pedido == null ? null : MapToPedidoDto(pedido);
        }

        public async Task<PedidoDto?> CreatePedidoAsync(PedidoCreateDto dto)
        {
            // Validar cliente y productos aquí...
            var pedido = new Pedido
            {
                IdCliente = dto.IdCliente,
                FechaEntrega = dto.FechaEntrega,
                Estado = "Pendiente",
                PedidoProductos = dto.Productos.Select(p => new PedidoProducto
                {
                    IdProducto = p.IdProducto,
                    Cantidad = p.Cantidad
                }).ToList()
            };

            await _repository.AddPedidoAsync(pedido);
            await _repository.SaveChangesAsync();

            var pedidoConsulta = await _repository.GetPedidoByIdAsync(pedido.IdPedido);

            return MapToPedidoDto(pedidoConsulta);
        }

        public async Task<bool> UpdatePedidoAsync(int id, PedidoUpdateDto dto)
        {
            try
            {
                var pedido = await _repository.GetPedidoByIdAsync(id);
                if (pedido == null || pedido.Estado == "Entregado") return false;

                // Actualiza la fecha de entrega
                pedido.FechaEntrega = dto.FechaEntrega ?? pedido.FechaEntrega;

                if (dto.Productos != null)
                {
                    // Elimina los productos actuales del pedido de forma explícita
                    var productosActuales = pedido.PedidoProductos.ToList();
                    foreach (var producto in productosActuales)
                    {
                        _repository.RemovePedidoProducto(producto); // Método para marcar entidad como eliminada
                    }

                    // Agrega los nuevos productos
                    foreach (var p in dto.Productos)
                    {
                        pedido.PedidoProductos.Add(new PedidoProducto
                        {
                            IdPedido = pedido.IdPedido, // importante para mantener la FK
                            IdProducto = p.IdProducto,
                            Cantidad = p.Cantidad
                        });
                    }
                }

                await _repository.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw; // (opcional: loguear excepción aquí)
            }
        }


        public async Task<bool> DeletePedidoAsync(int id)
        {
            var pedido = await _repository.GetPedidoByIdAsync(id);
            if (pedido == null || pedido.Estado != "Pendiente") return false;

            await _repository.DeletePedidoAsync(pedido);
            await _repository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<PedidoDto>> GetPedidosPorClienteAsync(int idCliente)
        {
            var pedidos = await _repository.GetPedidosPorClienteAsync(idCliente);
            return pedidos.Select(MapToPedidoDto);
        }

        public async Task<RouteAssignmentResponse?> AsignarRutasAsync(List<int> ids)
        {

            try
            {
                var pedidos = await _repository.GetPedidosPorIdsAsync(ids);
                if (pedidos.Any(p => p.Estado != "Pendiente")) return null;

                var request = pedidos.Select(p => new OrderAssignmentRequest
                {
                    IdOrder = p.IdPedido,
                    DeliveryDate = p.FechaEntrega,
                    Destination = p.IdClienteNavigation.Direccion
                }).ToList();

                var response = await _routeService.AssignRouteAsync(request);

                foreach (var a in response.Assignments)
                {
                    var pedido = pedidos.First(p => p.IdPedido == a.IdOrder);

                    var estadoRutaSaas = await _routeService.GetRouteStatusAsync(a.IdRoute);

                    var ruta = new Ruta
                    {
                        Estado = estadoRutaSaas.CurrentStatus,
                        FechaAsignacion = DateTime.UtcNow,
                        FechaEstimadaEntrega = a.EstimatedDelivery
                    };

                    // Guardar la ruta y obtener su IdRuta
                    ruta = await _repository.AgregarRutaAsync(ruta);

                    pedido.PedidoRutas.Add(new PedidoRutas
                    {
                        IdRuta = ruta.IdRuta,
                        FechaAsignacion = DateTime.UtcNow
                    });

                    var estadoPedidoHomologa = "Asignado";

                    if (estadoRutaSaas.CurrentStatus == "EnTránsito")
                    {
                        estadoPedidoHomologa = "EnTránsito";
                    }
                    else if (estadoRutaSaas.CurrentStatus == "Reportado")
                    {
                        estadoPedidoHomologa = "EnTránsito";
                    }
                    else if (estadoRutaSaas.CurrentStatus == "Novedad")
                    {
                        estadoPedidoHomologa = "EnTránsito";
                    }
                    else if (estadoRutaSaas.CurrentStatus == "Entregado")
                    {
                        estadoPedidoHomologa = "Entregado";
                    }

                    pedido.Estado = estadoPedidoHomologa;
                }

                await _repository.SaveChangesAsync();
                return response;
            }
            catch (Exception ex)
            {

                throw;
            }


        }

        public async Task<IEnumerable<PedidoPendienteDto>> ObtenerPedidosPendientesAsync()
        {
            var pedidos = await _repository.ObtenerPedidosPendientesAsync();

            return pedidos.Select(p => new PedidoPendienteDto
            {
                IdPedido = p.IdPedido,
                ClienteNombre = p.IdClienteNavigation.Nombre,
                DireccionEntrega = p.IdClienteNavigation.Direccion,
                FechaEntrega = p.FechaEntrega
            });
        }

        private PedidoDto MapToPedidoDto(Pedido pedido)
        {
            var pedidoDto = new PedidoDto
            {
                IdPedido = pedido.IdPedido,
                Cliente = new ClienteDto
                {
                    IdCliente = pedido.IdClienteNavigation.IdCliente,
                    Nombre = pedido.IdClienteNavigation.Nombre,
                    Email = pedido.IdClienteNavigation.Email,
                    Direccion = pedido.IdClienteNavigation.Direccion ?? ""
                },
                FechaCreacion = pedido.FechaCreacion ?? DateTime.Now,
                FechaEntrega = pedido.FechaEntrega,
                Estado = pedido.Estado,
                Productos = pedido.PedidoProductos.Select(pp => new ProductoPedidoDto
                {
                    IdProducto = pp.IdProducto,
                    Nombre = pp.IdProductoNavigation.Nombre,
                    Cantidad = pp.Cantidad,
                    Precio = pp.IdProductoNavigation.Precio
                }).ToList(),
                Rutas = pedido.PedidoRutas.Select(pr => new RutaDto
                {
                    IdRuta = pr.IdRuta,
                    Estado = pr.IdRutaNavigation.Estado,
                    FechaAsignacion = pr.FechaAsignacion ?? DateTime.Now, //validar


                }).ToList()
            };

            return pedidoDto;
        }

    }



}
