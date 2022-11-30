using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.Entidades
{
    public class Usuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
