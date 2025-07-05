// <copyright file="MappingProfile.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using AutoMapper;

namespace PruebaTecnicaSodimac.Application.Common.Profiles;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
    }

    protected internal MappingProfile(string profileName) : base(profileName)
    {
    }

    protected internal MappingProfile(string profileName,
        Action<IProfileExpression> configurationAction) : base(profileName,
        configurationAction)
    {
    }
}
