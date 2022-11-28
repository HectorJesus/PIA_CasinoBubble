using CasinoBubble.Validaciones;
using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.DTOs
{
    public class ObtenerParticipantesDTO
    {
        public int Id { get; set; }  //ID del participante

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }  //Nombre del participante

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 10, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Apellido { get; set; }  //Apellido del participante

        public int IdRifa { get; set; }
    }
}
