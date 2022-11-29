using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.Entidades
{
    public class Usuario
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } //Correo para registro
        public string Password { get; set; } //Contraseña para registro
    }
}
