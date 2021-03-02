using System.ComponentModel.DataAnnotations;

namespace Olimpia.Entidades
{
    public class Factura
    {
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten números de 0-9")]
        public int Id { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten números de 0-9")]
        public int Nit { get; set; }

        [Required]
        [RegularExpression(@"^[0-9a-zA-Z\sñÑáéíóúÁÉÍÓÚ.,-]+$", ErrorMessage = "Solo se permiten caracteres 0-9 y a-z")]
        public string Descripcion { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Solo se permiten números de 0-9")]
        public decimal ValorTotal { get; set; }

        [Required]
        [RegularExpression(@"^[0-9.]+$", ErrorMessage = "Solo se permiten números de 0-9")]
        public decimal PorcentIva { get; set; }
    }
}
