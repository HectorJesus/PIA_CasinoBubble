using CasinoBubble.Entidades;
using CasinoBubble.Validaciones;
using System.ComponentModel.DataAnnotations;


namespace CasinoBubble.Entidades
{
    public class Participante : IValidatableObject
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string ApellidoP { get; set; }

        [Required(ErrorMessage = "El campo {0} es necesario")]
        [StringLength(maximumLength: 15, ErrorMessage = "El campo {0} no puede tener mas de {1} caracteres")]
        [PrimeraLetraMayuscula]
        public string ApellidoM { get; set; }

        public DateTime? FechaInscripcion { get; set; }

        public int IdRifa { get; set; }
        public List<ParticipanteRifa> ParticipanteRifa { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                            new string[] { nameof(Nombre) });
                }
            }

            if (!string.IsNullOrEmpty(ApellidoP))
            {
                var primeraLetra = ApellidoP[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                            new string[] { nameof(ApellidoP) });
                }
            }

            if (!string.IsNullOrEmpty(ApellidoM))
            {
                var primeraLetra = ApellidoM[0].ToString();
                if (primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                            new string[] { nameof(ApellidoM) });
                }
            }
        }
    }
}

