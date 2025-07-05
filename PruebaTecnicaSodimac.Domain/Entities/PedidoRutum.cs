// <copyright file="PedidoRutum.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace ClassLibrary1.Data.Entities;

public partial class PedidoRutas
{
	public int PedidoIdRuta { get; set; }

	public int IdPedido { get; set; }

	public int IdRuta { get; set; }

	public DateTime? FechaAsignacion { get; set; }

	public virtual Pedido IdPedidoNavigation { get; set; } = null!;

	public virtual Ruta IdRutaNavigation { get; set; } = null!;
}
