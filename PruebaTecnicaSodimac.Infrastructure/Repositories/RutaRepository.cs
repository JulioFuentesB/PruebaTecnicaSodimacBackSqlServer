using ClassLibrary1.Data.Entities;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Infrastructure.Repositories
{
    public class RutaRepository : IRutaRepository
    {
        private readonly AppDbContext _context;

        public RutaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ruta>> GetAllAsync()
            => await _context.Ruta.ToListAsync();

        public async Task<Ruta?> GetByIdAsync(int id)
            => await _context.Ruta.FindAsync(id);

        public async Task<Ruta> CreateAsync(Ruta ruta)
        {
            _context.Ruta.Add(ruta);
            await _context.SaveChangesAsync();
            return ruta;
        }

        public async Task UpdateAsync(Ruta ruta)
        {
            _context.Entry(ruta).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Ruta ruta)
        {
            _context.Ruta.Remove(ruta);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> HasPedidosAsociadosAsync(int idRuta)
            => await _context.PedidoRuta.AnyAsync(pr => pr.IdRuta == idRuta);
    }
}
