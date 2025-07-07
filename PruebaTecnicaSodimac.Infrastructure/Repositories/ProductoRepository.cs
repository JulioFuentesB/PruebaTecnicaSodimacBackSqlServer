using ClassLibrary1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Infrastructure.Repositories
{
    public class ProductoRepository : IProductoRepository
    {
        private readonly AppDbContext _context;

        public ProductoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Producto>> ObtenerTodosAsync()
        {
            return await _context.Productos.ToListAsync();
        }

        public async Task<Producto?> ObtenerPorIdAsync(int id)
        {
            return await _context.Productos.FindAsync(id);
        }

        public async Task CrearAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
        }

        public async Task ActualizarAsync(Producto producto)
        {
            _context.Productos.Update(producto);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarAsync(Producto producto)
        {
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ExisteEnPedidosAsync(int idProducto)
        {
            return await _context.PedidoProductos.AnyAsync(pp => pp.IdProducto == idProducto);
        }

        // Métodos que ya tenías
        public async Task<IEnumerable<Producto>> GetAllAsync() =>
            await _context.Productos.ToListAsync();

        public async Task<Producto?> GetByIdAsync(int id) =>
            await _context.Productos.FindAsync(id);

        public async Task<Producto> CreateAsync(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return producto; // Ajustado para devolver el producto creado
        }

        public async Task UpdateAsync(Producto producto)
        {
            _context.Productos.Update(producto); // Cambiado de ActualizarAsync a UpdateAsync
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var producto = await GetByIdAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
                await _context.SaveChangesAsync();
            }
        }

        // Métodos faltantes
        public async Task<bool> ExistsAsync(int id) =>
            await _context.Productos.AnyAsync(e => e.IdProducto == id);

        public async Task<bool> SkuExistsAsync(string sku, int? excludeId = null)
        {
            return await _context.Productos
                .AnyAsync(p => p.Sku == sku && (excludeId == null || p.IdProducto != excludeId));
        }


    }

}
