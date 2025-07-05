// <copyright file="ReportesController.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ReportesController : ControllerBase
	{
		private readonly AppDbContext _context;

		public ReportesController(AppDbContext context)
		{
			_context = context;
		}

		[HttpGet("entregas")]
		public async Task<ActionResult<ReporteEntregasDto>> GetReporteEntregas(
			[FromQuery] DateTime? desde = null,
			[FromQuery] DateTime? hasta = null)
		{
			var fechaInicio = desde ?? DateTime.UtcNow.AddDays(-30);
			var fechaFin = hasta ?? DateTime.UtcNow;

			var entregas = await _context.Pedidos
				.Where(p => p.FechaCreacion >= fechaInicio && p.FechaCreacion <= fechaFin)
				.GroupBy(p => p.Estado)
				.Select(g => new
				{
					Estado = g.Key,
					Cantidad = g.Count()
				})
				.ToListAsync();

			var total = entregas.Sum(e => e.Cantidad);

			var reporte = new ReporteEntregasDto
			{
				Periodo = $"{fechaInicio:yyyy-MM-dd} al {fechaFin:yyyy-MM-dd}",
				Totales = entregas
			.Where(e => !string.IsNullOrEmpty(e.Estado))
			.ToDictionary(
			e => e.Estado!,
			e => (dynamic)new
			{
				Cantidad = e.Cantidad,
				Porcentaje = total > 0 ? Math.Round((double)e.Cantidad / total * 100, 2) : 0
			})
			};

			return Ok(reporte);
		}
	}
}
