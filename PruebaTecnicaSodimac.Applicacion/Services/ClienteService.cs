using ClassLibrary1.Data.Entities;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services
{
    public class ClienteService : IClienteService
    {
        private readonly IClienteRepository _repository;

        public ClienteService(IClienteRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ClienteDto>> ObtenerTodosAsync()
        {
            var clientes = await _repository.GetAllAsync();
            return clientes.Select(MapToDto).ToList();
        }

        public async Task<ClienteDto?> ObtenerPorIdAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id);
            return cliente == null ? null : MapToDto(cliente);
        }

        public async Task<ClienteDto> CrearAsync(ClienteCreateDto dto)
        {
            var cliente = new Cliente
            {
                Nombre = dto.Nombre,
                Direccion = dto.Direccion,
                Email = dto.Email,
                Telefono = dto.Telefono
            };

            var clienteCreado = await _repository.CreateAsync(cliente);
            return MapToDto(clienteCreado);
        }

        public async Task ActualizarAsync(int id, ClienteUpdateDto dto)
        {
            var cliente = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cliente no encontrado");

            cliente.Nombre = dto.Nombre ?? cliente.Nombre;
            cliente.Direccion = dto.Direccion ?? cliente.Direccion;
            cliente.Email = dto.Email ?? cliente.Email;
            cliente.Telefono = dto.Telefono ?? cliente.Telefono;

            await _repository.UpdateAsync(cliente);
        }

        public async Task EliminarAsync(int id)
        {
            var cliente = await _repository.GetByIdAsync(id) ?? throw new KeyNotFoundException("Cliente no encontrado");

            if (await _repository.TienePedidosAsociadosAsync(id))
                throw new InvalidOperationException("No se puede eliminar un cliente con pedidos asociados");

            await _repository.DeleteAsync(cliente);
        }

        // Método de mapeo manual
        private static ClienteDto MapToDto(Cliente cliente)
        {
            return new ClienteDto
            {
                IdCliente = cliente.IdCliente,
                Nombre = cliente.Nombre,
                Email = cliente.Email,
                Direccion = cliente.Direccion
            };
        }
    }
}
