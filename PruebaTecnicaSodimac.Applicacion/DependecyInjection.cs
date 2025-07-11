// <copyright file="DependecyInjection.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Services.Serilog;
using PruebaTecnicaSodimac.Application.Common.Profiles;
using PruebaTecnicaSodimac.Application.Services;
using PruebaTecnicaSodimac.Application.Services.Serilog;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace PruebaTecnicaSodimac.Application;

[ExcludeFromCodeCoverage]
public static class DependecyInjection
{
    public static IServiceCollection AddApplication(
        this IServiceCollection services)
    {
        _ = services.AddFluentValidationAutoValidation()
            .AddFluentValidationClientsideAdapters();

        _ = services.AddAutoMapper(
            Assembly.GetAssembly(typeof(MappingProfile)));


        services.AddTransient<ISerilogImplements, SerilogImplements>();
        services.AddTransient<IRouteService, SimulatedRouteService>();
        services.AddTransient<IPedidoService, PedidoService>();
        services.AddTransient<IRutaService, RutaService>();
        services.AddTransient<IClienteService, ClienteService>();
        services.AddTransient<IProductoService, ProductoService>();
        services.AddTransient<IReporteService, ReporteService>();



        return services;
    }
}
