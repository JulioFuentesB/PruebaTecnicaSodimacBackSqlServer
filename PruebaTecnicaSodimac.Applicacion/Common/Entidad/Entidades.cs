// <copyright file="Entidades.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaSodimac.Application.Common.Entidad
{
    //// OrderManagement.Core/Entities/Cliente.cs
    //public class Cliente
    //{
    //	public int IdCliente { get; set; }
    //	[Required, MaxLength(100)]
    //	public string Nombre { get; set; }
    //	[Required, MaxLength(200)]
    //	public string Direccion { get; set; }
    //	[Required, EmailAddress]
    //	public string Email { get; set; }
    //	public string Telefono { get; set; }
    //	public ICollection<Pedido> Pedidos { get; set; }
    //}

    //// OrderManagement.Core/Entities/Pedido.cs
    //public class Pedido
    //{
    //	public int IdPedido { get; set; }
    //	[ForeignKey("Cliente")]
    //	public int IdCliente { get; set; }
    //	public DateTime FechaCreacion { get; set; } = DateTime.UtcNow;
    //	[Required]
    //	public DateTime FechaEntrega { get; set; }
    //	public string Estado { get; set; } = "Pendiente";

    //	public Cliente Cliente { get; set; }
    //	public ICollection<PedidoProducto> Productos { get; set; }
    //	public ICollection<PedidoRuta> Rutas { get; set; }
    //}

    //// OrderManagement.Core/Entities/PedidoRuta.cs
    //public class PedidoRuta
    //{
    //	public int PedidoIdRuta { get; set; }
    //	[ForeignKey("Pedido")]
    //	public int IdPedido { get; set; }
    //	[ForeignKey("Ruta")]
    //	public int IdRuta { get; set; }
    //	public DateTime FechaAsignacion { get; set; } = DateTime.UtcNow;

    //	public Pedido Pedido { get; set; }
    //	public Ruta Ruta { get; set; }
    //}

    // DTOs para Pedidos
    public class PedidoDto
    {
        public int IdPedido { get; set; }
        public ClienteDto Cliente { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaEntrega { get; set; }
        public string Estado { get; set; }
        public List<ProductoPedidoDto> Productos { get; set; }
        public List<RutaDto> Rutas { get; set; }
    }

    public class PedidoCreateDto
    {
        [Required]
        public int IdCliente { get; set; }

        [Required]
        public DateTime FechaEntrega { get; set; }

        [Required]
        [MinLength(1)]
        public List<ProductoPedidoCreateDto> Productos { get; set; }
    }

    public class PedidoUpdateDto
    {
        public DateTime? FechaEntrega { get; set; }
        public List<ProductoPedidoCreateDto>? Productos { get; set; }
    }

    // DTOs para Clientes
    public class ClienteDto
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Direccion { get; set; } = null!;
    }

    // DTOs para Productos
    public class ProductoPedidoDto
    {
        public int IdProducto { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }
    }

    public class ProductoPedidoCreateDto
    {
        [Required]
        public int IdProducto { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Cantidad { get; set; }
    }

    // DTOs para Rutas
    public class RutaDto
    {
        public int IdRuta { get; set; }
        public string Estado { get; set; }
        public DateTime FechaAsignacion { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
    }

    // DTOs para SaaS
    public class OrderAssignmentRequest
    {
        public int IdOrder { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string Destination { get; set; }
    }

    public class RouteAssignmentResponse
    {
        public List<RouteAssignment> Assignments { get; set; }
    }

    public class RouteAssignment
    {
        public int IdOrder { get; set; }
        public int IdRoute { get; set; }
        public DateTime EstimatedDelivery { get; set; }
    }

    public class RouteStatusResponse
    {
        public int IdRoute { get; set; }
        public string CurrentStatus { get; set; }
        public DateTime LastUpdate { get; set; }
    }

    // DTO para Reportes
    public class ReporteEntregasDto
    {
        public string Periodo { get; set; }
        public Dictionary<string, dynamic> Totales { get; set; }
    }

    // Clientes
    public class ClienteCreateDto
    {
        [Required, MaxLength(100)]
        public string Nombre { get; set; }

        [Required, MaxLength(200)]
        public string Direccion { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }

        public string Telefono { get; set; }
    }

    public class ClienteUpdateDto
    {
        public string? Nombre { get; set; }
        public string? Direccion { get; set; }
        public string? Email { get; set; }
        public string? Telefono { get; set; }
    }

    // Rutas
    public class RutaCreateDto
    {
        [Required]
        public DateTime FechaEstimadaEntrega { get; set; }
    }

    public class RutaUpdateDto
    {
        public string? Estado { get; set; }
        public DateTime? FechaEstimadaEntrega { get; set; }
    }

    public class PedidoPendienteDto
    {
        public int IdPedido { get; set; }
        public string? ClienteNombre { get; set; }
        public string? DireccionEntrega { get; set; }
        public DateTime FechaEntrega { get; set; }
    }

}
