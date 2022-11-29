using AutoMapper;
using CasinoBubble.DTOs;
using CasinoBubble.Entidades;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CasinoBubble.Controllers
{
    public class PremiosController : ControllerBase
    {
        public PremiosController()
        {



            [HttpGet("Obtener Ganadores Rifa")]
            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

            public async Task<ActionResult<ObtenerParticipantesDTO >>
            {


                try
                {
                    var numUsuarios = SELECT COUNT(*) ParticipanteRifa FROM CasinoBubble

                }
                if (numUsuarios == 0)
                {
                    return NotFound();
                }

            }
        }
    }
}
