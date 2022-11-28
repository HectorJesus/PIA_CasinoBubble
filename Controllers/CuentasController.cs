using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CasinoBubble.DTOs;
using CasinoBubble.Entidades;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace CasinoBubble.Controllers
{
    [ApiController]
    [Route("Cuentas")]
    public class CuentasController
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;
        public CuentasController(UserManager<IdentityUser> userManager, IConfiguration configuration,
            SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }
        private async Task<AutenticacionResp> ConstruirToken(Usuario sistemaUsuario)
        {

            var claims = new List<Claim>
            {
                new Claim("email", sistemaUsuario.Email)

            };

            var usuario = await userManager.FindByEmailAsync(sistemaUsuario.Email);
            var claimsDB = await userManager.GetClaimsAsync(usuario);

            claims.AddRange(claimsDB);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["keyjwt"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expiration = DateTime.UtcNow.AddMinutes(30);

            var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AutenticacionResp()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(securityToken),
                Expiracion = expiration
            };
        }

        [HttpPost("Registrar")]
        public async Task<ActionResult<AutenticacionResp>> Registrar(Usuario sistema)
        {
            var user = new IdentityUser { UserName = sistema.Email, Email = sistema.Email };
            var result = await userManager.CreateAsync(user, sistema.Password);

            if (result.Succeeded)
            {
                return await ConstruirToken(sistema);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("Login")]
        public async Task<ActionResult<AutenticacionResp>> Login(Usuario sistemaUsuario)
        {
            var result = await signInManager.PasswordSignInAsync(sistemaUsuario.Email,
                sistemaUsuario.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await ConstruirToken(sistemaUsuario);
            }
            else
            {
                return BadRequest("Login Incorrecto");
            }

        }

        

        [HttpPost("Reiniciar Contraseña")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<AutenticacionResp>> Renovar()
        {
            var emailClaim = HttpContext.User.Claims.Where(claim => claim.Type == "email").FirstOrDefault();
            var email = emailClaim.Value;

            var credenciales = new Usuario()
            {
                Email = email
            };

            return await ConstruirToken(credenciales);

        }

        [HttpPost("Crear Admin")]
        public async Task<ActionResult> HacerAdmin(EditarAdministradorDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.AddClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }

        [HttpPost("Revocar Admin")]
        public async Task<ActionResult> RemoverAdmin(EditarAdministradorDTO editarAdminDTO)
        {
            var usuario = await userManager.FindByEmailAsync(editarAdminDTO.Email);

            await userManager.RemoveClaimAsync(usuario, new Claim("EsAdmin", "1"));

            return NoContent();
        }
    }
}
