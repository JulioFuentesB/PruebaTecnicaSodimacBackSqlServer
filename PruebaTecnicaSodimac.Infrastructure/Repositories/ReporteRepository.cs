using Microsoft.EntityFrameworkCore;
using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Infrastructure.Context;
using System.Globalization;

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
                .Where(p => p.FechaCreacion.HasValue &&
                            p.FechaCreacion.Value.Date >= desde.Date &&
                            p.FechaCreacion.Value.Date <= hasta.Date)
                .GroupBy(p => p.Estado)
                .Select(g => new EntregaEstadoResumen
                {
                    Estado = g.Key,
                    Cantidad = g.Count()
                })
                .ToListAsync();

        }


        private DateTime ConvertirFechaManual(string fecha)
        {
            // Separar partes de la fecha
            var partes = fecha.Split('/');

            if (partes.Length != 3)
                throw new FormatException("La fecha no tiene un formato válido");

            // Interpretar como MM/dd/yyyy y reordenar a dd/MM/yyyy
            string dia = partes[1];
            string mes = partes[0];
            string anio = partes[2];

            string fechaReformateada = $"{dia}/{mes}/{anio}";

            // Parsear con el nuevo formato
            return DateTime.ParseExact(fechaReformateada, "dd/MM/yyyy", CultureInfo.InvariantCulture);
        }

    }

}
