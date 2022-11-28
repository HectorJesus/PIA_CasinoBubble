using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.DTOs
{
    public class ObtenerRifa
    {
        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 25, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        public string NombreRifa { get; set; }
    }
}
