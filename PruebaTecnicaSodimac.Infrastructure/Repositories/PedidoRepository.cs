// <copyright file="PedidoRepository.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using ClassLibrary1.Data.Entities;

using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Infrastructure.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _context;

        public PedidoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> GetPedidosAsync(int page, int pageSize) =>
            await _context.Pedidos
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.PedidoRutas).ThenInclude(pp => pp.IdRutaNavigation)
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.IdProductoNavigation)
                .OrderByDescending(p => p.FechaCreacion)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

        public async Task<Pedido?> GetPedidoByIdAsync(int id)
        {
            var pedidoss = await _context.Pedidos
                .ToListAsync();

            var resultado = await _context.Pedidos
        .Include(p => p.IdClienteNavigation)
        .Include(p => p.PedidoProductos)
            .ThenInclude(pp => pp.IdProductoNavigation)
         .Include(p => p.PedidoRutas).ThenInclude(pp => pp.IdRutaNavigation)
        .FirstOrDefaultAsync(p => p.IdPedido == id);
            return resultado;

        }

        public void RemovePedidoProducto(PedidoProducto pedidoProducto)
        {
            _context.PedidoProductos.Remove(pedidoProducto);
        }

        public async Task<List<Pedido>> GetPedidosPorClienteAsync(int idCliente) =>
            await _context.Pedidos
                .Where(p => p.IdCliente == idCliente)
               .Include(p => p.IdClienteNavigation)
               .Include(p => p.PedidoRutas).ThenInclude(pp => pp.IdRutaNavigation)
                .Include(p => p.PedidoProductos)
                    .ThenInclude(pp => pp.IdProductoNavigation)
                .ToListAsync();

        public async Task<List<Pedido>> GetPedidosPorIdsAsync(List<int> ids) =>
            await _context.Pedidos
                .Where(p => ids.Contains(p.IdPedido))
                .Include(p => p.IdClienteNavigation)
                .Include(p => p.PedidoRutas).ThenInclude(pp => pp.IdRutaNavigation)
                .ToListAsync();

        public async Task AddPedidoAsync(Pedido pedido) =>
            await _context.Pedidos.AddAsync(pedido);

        public async Task UpdatePedidoAsync(Pedido pedido) =>
            _context.Pedidos.Update(pedido);

        public async Task DeletePedidoAsync(Pedido pedido) =>
            _context.Pedidos.Remove(pedido);

        public async Task SaveChangesAsync() =>
            await _context.SaveChangesAsync();


        public async Task<IEnumerable<Pedido>> ObtenerPedidosPendientesAsync()
        {
            return await _context.Pedidos
                .Where(p => p.Estado == "Pendiente")
                .Include(p => p.IdClienteNavigation)
                //.Include(p => p.PedidoRutas).ThenInclude(pp => pp.IdRutaNavigation)
                .ToListAsync();
        }

        public async Task<Ruta> AgregarRutaAsync(Ruta ruta)
        {
            await _context.Ruta.AddAsync(ruta);
            await _context.SaveChangesAsync(); // Aqu� se genera el IdRuta
            return ruta; // Ya tendr� el IdRuta asignado
        }

    }

}
