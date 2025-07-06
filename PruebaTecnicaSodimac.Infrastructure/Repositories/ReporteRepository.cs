using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Infrastructure.Context;

namespace PruebaTecnicaSodimac.Infrastructure.Repositories
{
    public class ReporteRepository : IReporteRepository
    {
        private readonly AppDbContext _context;

        public ReporteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<EntregaEstadoResumen>> ConsultarEntregasPorEstado(DateTime desde, DateTime hasta)
        {
            return await _context.Pedidos
                .Where(p => p.FechaCreacion >= desde && p.FechaCreacion <= hasta)
                .GroupBy(p => p.Estado)
                .Select(g => new EntregaEstadoResumen
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();
        }
    }

}
