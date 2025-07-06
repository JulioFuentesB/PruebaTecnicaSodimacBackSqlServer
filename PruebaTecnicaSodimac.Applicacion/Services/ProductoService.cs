using ClassLibrary1.Data.Entities;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services
{
    public class ProductoService : IProductoService
    {
        private readonly IProductoRepository _productoRepository;

        public ProductoService(IProductoRepository productoRepository)
        {
            _productoRepository = productoRepository;
        }

        public async Task<IEnumerable<ProductoDto>> ObtenerTodosAsync()
        {
            var productos = await _productoRepository.ObtenerTodosAsync();

            return productos.Select(p => new ProductoDto
            {
                IdProducto = p.IdProducto,
                Nombre = p.Nombre,
                Sku = p.Sku,
                Precio = p.Precio
            });
        }

        public async Task<ProductoDto?> ObtenerPorIdAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto == null) return null;

            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Sku = producto.Sku,
                Precio = producto.Precio
            };
        }

        public async Task<ProductoDto> CrearAsync(ProductoCreateDto dto)
        {
            var producto = new Producto
            {
                Nombre = dto.Nombre,
                Sku = dto.Sku,
                Precio = dto.Precio
            };

            await _productoRepository.CrearAsync(producto);

            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Sku = producto.Sku,
                Precio = producto.Precio
            };
        }

        public async Task ActualizarAsync(int id, ProductoUpdateDto dto)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto == null) throw new KeyNotFoundException();

            producto.Nombre = dto.Nombre ?? producto.Nombre;
            producto.Sku = dto.Sku ?? producto.Sku;
            producto.Precio = dto.Precio ?? producto.Precio;

            await _productoRepository.ActualizarAsync(producto);
        }

        public async Task EliminarAsync(int id)
        {
            var producto = await _productoRepository.ObtenerPorIdAsync(id);
            if (producto == null) throw new KeyNotFoundException();

            if (await _productoRepository.ExisteEnPedidosAsync(id))
                throw new InvalidOperationException("No se puede eliminar un producto que está en pedidos");

            await _productoRepository.EliminarAsync(producto);
        }
    }
}
