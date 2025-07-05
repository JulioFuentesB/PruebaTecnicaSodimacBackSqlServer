// <copyright file="GeneralException.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.Exceptions;

/// <summary>
///     General Exception
/// </summary>
public class GeneralException : Exception
{
	private const string DefaultMessage = "La entidad no existe.";
	public GeneralException() : base(DefaultMessage) { }
	public GeneralException(string message) : base(message) { }

	public GeneralException(string message, Exception innerException) : base(
		message, innerException)
	{
	}
}
