using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.DTOs
{
    public class EditarAdministradorDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
}
