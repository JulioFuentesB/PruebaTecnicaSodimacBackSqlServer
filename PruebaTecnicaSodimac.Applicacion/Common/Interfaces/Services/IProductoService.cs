// <copyright file="IProductoService.cs" company="prueba">
// 	Copyright (c) prueba.
// 	All Rights Reserved.  Licensed under the Apache License, Version 2.0.
// 	See License.txt in the project root for license information.
// </copyright>

using PruebaTecnicaSodimac.Application.Common.Entidad;

namespace PruebaTecnicaSodimac.Application.Common.Interfaces.Services
{
    public interface IProductoService
    {
        Task<IEnumerable<ProductoDto>> ObtenerTodosAsync();
        Task<ProductoDto?> ObtenerPorIdAsync(int id);
        Task<ProductoDto> CrearAsync(ProductoCreateDto dto);
        Task ActualizarAsync(int id, ProductoUpdateDto dto);
        Task EliminarAsync(int id);
        Task<ResultadoValidacionSKU> VerificarSkuUnicoAsync(string sku, int? idProducto = null);
    }

}
