using System.ComponentModel.DataAnnotations;
using CasinoBubble.Validaciones;
namespace CasinoBubble.Entidades
{
    public class Participante
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
        
        [Required(ErrorMessage = "El campo {0} es necesario")]
        public DateTime FechaInscripcion { get; set; }  //Fecha de inscripcion (Toma la fecha actual de tu sistema)

        public List<ParticipanteRifa> ParticipantesRifa { get; set; }


    }
}
