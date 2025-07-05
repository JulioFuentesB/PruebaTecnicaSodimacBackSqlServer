// <copyright file="SimulatedRouteService.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services
{
	public class SimulatedRouteService : IRouteService
	{
		public async Task<RouteAssignmentResponse> AssignRouteAsync(List<OrderAssignmentRequest> orders)
		{
			// Simulación de asignación de rutas
			await Task.Delay(200); // Simular latencia de red

			return new RouteAssignmentResponse
			{
				Assignments = orders.Select(o => new RouteAssignment
				{
					IdOrder = o.IdOrder,
					IdRoute = new Random().Next(1000, 9999),
					EstimatedDelivery = o.DeliveryDate.AddDays(new Random().Next(1, 3))
				}).ToList()
			};
		}

		public async Task<RouteStatusResponse> GetRouteStatusAsync(int IdRoute)
		{
			// Simulación de consulta de estado
			await Task.Delay(100);

			var statuses = new[] { "EnTránsito", "Reportado", "Novedad", "Entregado" };

			return new RouteStatusResponse
			{
				IdRoute = IdRoute,
				CurrentStatus = statuses[new Random().Next(statuses.Length)],
				LastUpdate = DateTime.UtcNow.AddMinutes(-new Random().Next(10, 120))
			};
		}
	}


}
