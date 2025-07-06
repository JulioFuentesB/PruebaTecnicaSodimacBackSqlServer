// <copyright file="IPedidoRepository.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using ClassLibrary1.Data.Entities;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Repository
{
    public interface IPedidoRepository
    {
        Task<List<Pedido>> GetPedidosAsync(int page, int pageSize);
        Task<Pedido?> GetPedidoByIdAsync(int id);
        Task AddPedidoAsync(Pedido pedido);
        Task UpdatePedidoAsync(Pedido pedido);
        Task DeletePedidoAsync(Pedido pedido);
        Task<List<Pedido>> GetPedidosPorClienteAsync(int idCliente);
        Task<List<Pedido>> GetPedidosPorIdsAsync(List<int> ids);
        Task SaveChangesAsync();
        Task<IEnumerable<Pedido>> ObtenerPedidosPendientesAsync();
        void RemovePedidoProducto(PedidoProducto pedidoProducto);
        Task<Ruta> AgregarRutaAsync(Ruta ruta);
    }




}
