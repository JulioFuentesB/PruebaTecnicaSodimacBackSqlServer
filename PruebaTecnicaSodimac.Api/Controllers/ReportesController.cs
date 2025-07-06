// <copyright file="ReportesController.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Microsoft.AspNetCore.Mvc;

using PruebaTecnicaSodimac.Application.Common.Entidad;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

namespace PruebaTecnicaSodimac.Api.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ReportesController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReportesController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("entregas")]
        [ProducesResponseType(typeof(ReporteEntregasDto), StatusCodes.Status200OK)]
        public async Task<ActionResult<ReporteEntregasDto>> GetReporteEntregas(
            [FromQuery] DateTime? desde = null,
            [FromQuery] DateTime? hasta = null)
        {
            var reporte = await _reporteService.GenerarReporteEntregasAsync(desde, hasta);
            return Ok(reporte);
        }
    }


}