// <copyright file="Pedido.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace ClassLibrary1.Data.Entities;

public partial class Pedido
{
	public int IdPedido { get; set; }

	public int IdCliente { get; set; }

	public DateTime? FechaCreacion { get; set; }

	public DateTime FechaEntrega { get; set; }

	public string? Estado { get; set; }

	public virtual Cliente IdClienteNavigation { get; set; } = null!;

	public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();

	public virtual ICollection<PedidoRutas> PedidoRutas { get; set; } = new List<PedidoRutas>();
}
