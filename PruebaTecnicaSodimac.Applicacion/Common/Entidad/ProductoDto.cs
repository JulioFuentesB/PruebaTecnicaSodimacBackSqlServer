using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaSodimac.Application.Common.Entidad
{
    // DTO para Producto
    public class ProductoDto
    {
        public int IdProducto { get; set; }
        public string? Nombre { get; set; }
        public string? Sku { get; set; }
        public decimal Precio { get; set; }
    }


    public class ProductoCreateDto
    {
        [Required, MaxLength(100)]
        public string? Nombre { get; set; }

        [Required, MaxLength(50)]
        public string? Sku { get; set; }

        [Required, Range(0.01, double.MaxValue)]
        public decimal Precio { get; set; }
    }

    public class ProductoUpdateDto
    {
        [MaxLength(100)]
        public string? Nombre { get; set; }

        [MaxLength(50)]
        public string? Sku { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal? Precio { get; set; }
    }
}
