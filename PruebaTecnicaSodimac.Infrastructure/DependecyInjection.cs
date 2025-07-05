// <copyright file="DependecyInjection.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PruebaTecnicaSodimac.Application.Common.Interfaces.Repository;
using PruebaTecnicaSodimac.Application.Common.Struct;
using PruebaTecnicaSodimac.Domain;
using PruebaTecnicaSodimac.Infrastructure.Context;
using PruebaTecnicaSodimac.Infrastructure.Repositories.App;

namespace PruebaTecnicaSodimac.Infrastructure;

public static class DependecyInjection
{

	public static void AddDbContext(this WebApplicationBuilder builder)
	{

		AddHacvSglDbContextFactory(builder);


	}

	private static void AddHacvSglDbContextFactory(WebApplicationBuilder builder)
	{
		builder?.Services.AddDbContextFactory<AppDbContext>(options =>
		{
			ConfigureDbContextOptions(builder, options, ""/*ConfigurationStruct.SGL_HCAV*/);
		});
        var conexion = "Server=AC-025\\SQLEXPRESS;Database=PruebaTecnicaS;Trusted_Connection=True;TrustServerCertificate=True;";



    //    builder.Services.AddDbContext<AppDbContext>(opciones =>
    //opciones.UseSqlServer("name=DefaultConnection", sqlServer =>sqlServer.UseNetTopologySuite()));

    }

	private static void ConfigureDbContextOptions(WebApplicationBuilder builder, DbContextOptionsBuilder options, string connectionStringName)
	{

		var conexion = "Server=AC-025\\SQLEXPRESS;Database=PruebaTecnicaS;Trusted_Connection=True;TrustServerCertificate=True;";
		// Environment.GetEnvironmentVariable(connectionStringName);

		options.UseSqlServer(conexion)
		.ConfigureWarnings(b => b.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.SqlServerEventId.DecimalTypeDefaultWarning));

		if (builder!.Environment.IsDevelopment()!)
		{
			// Configurar el nivel de registro
			options.EnableSensitiveDataLogging(); // Esto habilita la información sensible como parámetros de SQL
			options.LogTo(Console.WriteLine, new[] { DbLoggerCategory.Database.Command.Name }); // Esto redirige los mensajes de registro a la consola
		}
	}

	public static IServiceCollection AddInfrastructure(
		this IServiceCollection services, IConfiguration configuration)
	{
		var ListOrigin = Environment.GetEnvironmentVariable(ConfigurationStruct.WithOrigins)
			?.Split(',').ToList();
		ListOrigin = ListOrigin == null ? new List<string>() : ListOrigin;
		//cors
		services.AddCors(options =>
		{
			options.AddPolicy(ConfigurationStruct.CorsPolicy,
				builder => builder
					.AllowAnyMethod()
					.AllowAnyHeader()
					.WithOrigins("*"/*ListOrigin.ToArray()*/));
		});


		services.Configure<AppSettings>(options => configuration.Bind(options));

		services.AddTransient<IPedidoRepository, PedidoRepository>();
        services.AddTransient<IRutaRepository, RutaRepository>();



        return services;
	}


}
