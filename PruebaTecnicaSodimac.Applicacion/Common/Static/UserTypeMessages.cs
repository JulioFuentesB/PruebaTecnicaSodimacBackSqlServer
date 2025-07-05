// <copyright file="UserTypeMessages.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.Static;

/// <summary>
///     Mensajes de control de errores de la aplicación
/// </summary>
public static class UserTypeMessages
{
	public const string ERRGEN01 =
		"ERRGEN01 - Ocurrió un error general en la aplicación";

	public const string ERRGEN02 =
		"ERRGEN02 - ERROR! de excepcion en el metodo {0} | Parametros: {1} | Mensaje: {2}";

	public const string ERRGEN03 =
		"ERRGEN03 - ERROR! Error en el modelo de entrada";

	public const string ERRGEN04 =
		"ERRGEN04 - Critical! En el metodo {0} | Parametros: {1} | Mensaje: {2}";

	public const string ERRGEN05 =
		"ERRGEN05 - Warning! En el metodo {0} | Parametros: {1} | Mensaje: {2}";

	public const string INFGENO01 = "INFGENO01 - Procesamiento del metodo {0}";

	public const string INFGENO02 =
		"INFGENO02 - Procesamiento del metodo {0} | Parametros: {1} | Estado: {2}";

	public const string INFGENO03 =
		"INFGENO03 - Procesamiento del metodo {0} | Parametros: {1} | Estado del metodo: {2}";
}
