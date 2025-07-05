// <copyright file="UserQueryFilter.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

namespace PruebaTecnicaSodimac.Application.Common.QueryFilter;

public class UserQueryFilter
{
    public DateTime? fechaInicio { get; set; }

    public DateTime? fechaFin { get; set; }

    public int pageSize { get; set; }

    public int pageNumber { get; set; }
}
