// <copyright file="IGenericServiceAgent.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services.Http;

public interface IGenericServiceAgent
{
	Task<T?> PostAsync<T>(string url, object body,
		string contentType = "application/json",
		CancellationToken cancellationToken = default);

	Task<T?> PostAsync<T>(string url, object body,
		string? SubscriptionKey = null, string contentType = "application/json",
		CancellationToken cancellationToken = default);

	Task<T?> PutAsync<T>(string url, object body,
		string contentType = "application/json",
		CancellationToken cancellationToken = default);

	Task<T?> GetAsync<T>(string url, string contentType = "application/json",
		CancellationToken cancellationToken = default);
}
