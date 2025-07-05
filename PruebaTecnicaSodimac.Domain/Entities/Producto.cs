// <copyright file="Producto.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace ClassLibrary1.Data.Entities;

public partial class Producto
{
	public int IdProducto { get; set; }

	public string Nombre { get; set; } = null!;

	public string Sku { get; set; } = null!;

	public string? Descripcion { get; set; }

	public decimal Precio { get; set; }

	public virtual ICollection<PedidoProducto> PedidoProductos { get; set; } = new List<PedidoProducto>();
}
