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
                Precio = p.Precio,
                Descripcion = p.Descripcion
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
                Precio = producto.Precio,
                Descripcion = producto.Descripcion
            };
        }

        public async Task<ProductoDto> CrearAsync(ProductoCreateDto dto)
        {
            try
            {
                // Validación de SKU único
                if (await _productoRepository.SkuExistsAsync(dto.Sku))
                {
                    throw new InvalidOperationException("El SKU ya existe en la base de datos");
                }

                var producto = new Producto
                {
                    Nombre = dto.Nombre,
                    Sku = dto.Sku,
                    Precio = dto.Precio,
                    Descripcion = dto.Descripcion,
                };

                await _productoRepository.CreateAsync(producto);

                return MapToDto(producto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task ActualizarAsync(int id, ProductoUpdateDto dto)
        {
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto == null) throw new KeyNotFoundException("Producto no encontrado");

                // Validación de SKU único excluyendo el producto actual
                if (!string.IsNullOrEmpty(dto.Sku) &&
                    await _productoRepository.SkuExistsAsync(dto.Sku, id))
                {
                    throw new InvalidOperationException("El SKU ya existe en la base de datos");
                }

                producto.Nombre = dto.Nombre ?? producto.Nombre;
                producto.Sku = dto.Sku ?? producto.Sku;
                producto.Precio = dto.Precio ?? producto.Precio;
                producto.Descripcion = dto.Descripcion ?? producto.Descripcion;
                //producto.FechaActualizacion = DateTime.UtcNow;

                await _productoRepository.UpdateAsync(producto);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task EliminarAsync(int id)
        {
            try
            {
                var producto = await _productoRepository.GetByIdAsync(id);
                if (producto == null) throw new KeyNotFoundException("Producto no encontrado");

                if (await _productoRepository.ExisteEnPedidosAsync(id))
                    throw new InvalidOperationException("No se puede eliminar un producto que está en pedidos");

                await _productoRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<ResultadoValidacionSKU> VerificarSkuUnicoAsync(string sku, int? idProducto = null)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(sku))
                {
                    return new ResultadoValidacionSKU(false, sku, "El SKU no puede estar vacío");
                }

                var existe = await _productoRepository.SkuExistsAsync(sku, idProducto);

                return new ResultadoValidacionSKU(!existe, sku, existe ? "El SKU ya está en uso" : "SKU disponible");
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        // Método de mapeo privado para reutilización
        private ProductoDto MapToDto(Producto producto)
        {
            return new ProductoDto
            {
                IdProducto = producto.IdProducto,
                Nombre = producto.Nombre,
                Sku = producto.Sku,
                Precio = producto.Precio,
                Descripcion = producto.Descripcion,
            };
        }
    }
}
