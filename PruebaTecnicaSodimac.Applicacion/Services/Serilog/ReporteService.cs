using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Application.Services.Serilog
{
    public class ReporteService : IReporteService
    {
        private readonly IReporteRepository _reporteRepository;

        public ReporteService(IReporteRepository reporteRepository)
        {
            _reporteRepository = reporteRepository;
        }

        public async Task<ReporteEntregasDto> GenerarReporteEntregasAsync(DateTime? desde, DateTime? hasta)
        {
            var fechaInicio = desde ?? DateTime.UtcNow.AddDays(-30);
            var fechaFin = hasta ?? DateTime.UtcNow;

            var entregas = await _reporteRepository.ConsultarEntregasPorEstado(fechaInicio, fechaFin);

            var total = entregas.Sum(e => e.Cantidad);

            return new ReporteEntregasDto
            {
                Periodo = $"{fechaInicio:yyyy-MM-dd} al {fechaFin:yyyy-MM-dd}",
                Totales = entregas
                    .Where(e => !string.IsNullOrEmpty(e.Estado))
                    .ToDictionary(
                        e => e.Estado!,
                        e => (dynamic)new
                        {
                            Cantidad = e.Cantidad,
                            Porcentaje = total > 0 ? Math.Round((double)e.Cantidad / total * 100, 2) : 0
                        })
            };
        }
    }

}
