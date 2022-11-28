using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CasinoBubble.Controllers
{
    [ApiController]
    [Route("Participantes")]
    public class ParticipantesController
    {
        [HttpGet("Listado Participantes")]

        [HttpGet] //Busqueda por ID

        [HttpPost]

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
        [HttpPut("{id:int}")]

        [HttpDelete]

        [HttpPatch]

    }
}
