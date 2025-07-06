// <copyright file="IRutaRepository.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using ClassLibrary1.Data.Entities;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Repository
{
    public interface IRutaRepository
    {
        Task<IEnumerable<Ruta>> GetAllAsync();
        Task<Ruta?> GetByIdAsync(int id);
        Task<Ruta> CreateAsync(Ruta ruta);
        Task UpdateAsync(Ruta ruta);
        Task DeleteAsync(Ruta ruta);
        Task<bool> HasPedidosAsociadosAsync(int idRuta);
    }




}
