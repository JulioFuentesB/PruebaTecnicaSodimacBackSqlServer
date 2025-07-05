// <copyright file="dto.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaSodimac.Application.Common.Models.Dtos
{
	// DTOs para Pedidos
	public class PedidoDto
	{
		public int PedidoID { get; set; }
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
		public int ClienteID { get; set; }

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
		public int ClienteID { get; set; }
		public string Nombre { get; set; }
		public string Email { get; set; }
	}

	// DTOs para Productos
	public class ProductoPedidoDto
	{
		public int ProductoID { get; set; }
		public string Nombre { get; set; }
		public int Cantidad { get; set; }
		public decimal Precio { get; set; }
	}

	public class ProductoPedidoCreateDto
	{
		[Required]
		public int ProductoID { get; set; }

		[Required]
		[Range(1, int.MaxValue)]
		public int Cantidad { get; set; }
	}

	// DTOs para Rutas
	public class RutaDto
	{
		public int RutaID { get; set; }
		public string Estado { get; set; }
		public DateTime FechaAsignacion { get; set; }
	}

	// DTOs para SaaS
	public class OrderAssignmentRequest
	{
		public int OrderId { get; set; }
		public DateTime DeliveryDate { get; set; }
		public string Destination { get; set; }
	}

	public class RouteAssignmentResponse
	{
		public List<RouteAssignment> Assignments { get; set; }
	}

	public class RouteAssignment
	{
		public int OrderId { get; set; }
		public int RouteId { get; set; }
		public DateTime EstimatedDelivery { get; set; }
	}

	public class RouteStatusResponse
	{
		public int RouteId { get; set; }
		public string CurrentStatus { get; set; }
		public DateTime LastUpdate { get; set; }
	}

	// DTO para Reportes
	public class ReporteEntregasDto
	{
		public string Periodo { get; set; }
		public Dictionary<string, dynamic> Totales { get; set; }
	}
}
