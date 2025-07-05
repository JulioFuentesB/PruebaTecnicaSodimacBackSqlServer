// <copyright file="IHttpClientDomainService.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services.Http;

public interface IHttpClientDomainService
{
    Task<HttpResponseMessage> SendAsync<T>(HttpRequestMessage request,
        CancellationToken cancellationToken);
}
