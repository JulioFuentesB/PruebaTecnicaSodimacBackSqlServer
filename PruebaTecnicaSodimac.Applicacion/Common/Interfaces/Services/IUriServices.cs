// <copyright file="IUriServices.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using PruebaTecnicaSodimac.Application.Common.QueryFilter;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services;

public interface IUriServices
{
	Uri GetUserPaginationUri(UserQueryFilter filter, string actionUrl);
}
