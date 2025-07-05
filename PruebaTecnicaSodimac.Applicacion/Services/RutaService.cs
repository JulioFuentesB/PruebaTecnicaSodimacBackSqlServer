using ClassLibrary1.Data.Entities;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services
{
    public class RutaService : IRutaService
    {
        private readonly IRutaRepository _repository;

        public RutaService(IRutaRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<RutaDto>> ObtenerTodasAsync()
        {
            var rutas = await _repository.GetAllAsync();
            return rutas.Select(MapToDto).ToList();
        }

        public async Task<RutaDto?> ObtenerPorIdAsync(int id)
        {
            var ruta = await _repository.GetByIdAsync(id);
            return ruta == null ? null : MapToDto(ruta);
        }

        public async Task<RutaDto> CrearAsync(RutaCreateDto dto)
        {
            var ruta = new Ruta
            {
                Estado = "EnTránsito",
                FechaAsignacion = DateTime.UtcNow,
                FechaEstimadaEntrega = dto.FechaEstimadaEntrega
            };

            var rutaCreada = await _repository.CreateAsync(ruta);
            return MapToDto(rutaCreada);
        }

        public async Task ActualizarAsync(int id, RutaUpdateDto dto)
        {
            var ruta = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Ruta no encontrada");

            ruta.Estado = dto.Estado ?? ruta.Estado;
            ruta.FechaEstimadaEntrega = dto.FechaEstimadaEntrega ?? ruta.FechaEstimadaEntrega;

            await _repository.UpdateAsync(ruta);
        }

        public async Task EliminarAsync(int id)
        {
            var ruta = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Ruta no encontrada");

            if (await _repository.HasPedidosAsociadosAsync(id))
                throw new InvalidOperationException("No se puede eliminar una ruta con pedidos asignados");

            await _repository.DeleteAsync(ruta);
        }

        // Método de mapeo manual
        private static RutaDto MapToDto(Ruta ruta)
        {
            return new RutaDto
            {
                IdRuta = ruta.IdRuta,
                Estado = ruta.Estado,
                FechaAsignacion = ruta.FechaAsignacion,
                FechaEstimadaEntrega = ruta.FechaEstimadaEntrega
            };
        }
    }
}
