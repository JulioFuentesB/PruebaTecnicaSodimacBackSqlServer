// <copyright file="Rutum.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace ClassLibrary1.Data.Entities;

public partial class Ruta
{
	public int IdRuta { get; set; }

	public string Estado { get; set; } = null!;

	public DateTime FechaAsignacion { get; set; }

	public DateTime? FechaEstimadaEntrega { get; set; }

	public virtual ICollection<PedidoRutas> PedidoRuta { get; set; } = new List<PedidoRutas>();
}
