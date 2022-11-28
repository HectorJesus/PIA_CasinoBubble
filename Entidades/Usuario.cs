using System.ComponentModel.DataAnnotations;

namespace CasinoBubble.Entidades
{
    public class Usuario
    {
        [Required]
        [EmailAddress]
        [Required]
        public string Email { get; set; } //Correo para registro
        public string Password { get; set; } //Contraseña para registro
}
