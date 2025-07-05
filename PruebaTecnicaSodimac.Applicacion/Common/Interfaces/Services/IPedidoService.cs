// <copyright file="IPedidoService.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using PruebaTecnicaSodimac.Application.Common.Entidad;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services
{
	public interface IPedidoService
	{
		Task<IEnumerable<PedidoDto>> GetPedidosAsync(int page, int pageSize);
		Task<PedidoDto?> GetPedidoAsync(int id);
		Task<PedidoDto?> CreatePedidoAsync(PedidoCreateDto dto);
		Task<bool> UpdatePedidoAsync(int id, PedidoUpdateDto dto);
		Task<bool> DeletePedidoAsync(int id);
		Task<IEnumerable<PedidoDto>> GetPedidosPorClienteAsync(int idCliente);
		Task<RouteAssignmentResponse?> AsignarRutasAsync(List<int> ids);
	}

    public interface IRutaService
    {
        Task<IEnumerable<RutaDto>> ObtenerTodasAsync();
        Task<RutaDto?> ObtenerPorIdAsync(int id);
        Task<RutaDto> CrearAsync(RutaCreateDto dto);
        Task ActualizarAsync(int id, RutaUpdateDto dto);
        Task EliminarAsync(int id);
    }
}
