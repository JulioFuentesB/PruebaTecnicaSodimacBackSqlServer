// <copyright file="IPingServices.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using PruebaTecnicaSodimac.Domain;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services.Ping;

public interface IPingServices
{
	/// <summary>
	///     Version de la Api
	/// </summary>
	/// <returns></returns>
	Task<string?> Version();

	/// <summary>
	/// Prueba de conexion
	/// </summary>
	/// <param name="secreto"></param>
	/// <returns></returns>
	public Task<string?> SecretConexion(string secreto);

	/// <summary>
	///     Obtener la configuración del app settings
	/// </summary>
	/// <param name="database"></param>
	/// <returns></returns>
	public AppSettings GetAppsettings();
}
