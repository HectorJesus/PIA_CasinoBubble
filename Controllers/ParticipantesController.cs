using CasinoBubble.Entidades;
using CasinoBubble.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CasinoBubble.Controllers
{
    [ApiController]
    [Route("Participantes")]
    public class ParticipantesController
    {
        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger, IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]

        [HttpGet("Listado Participantes")]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            logger.LogInformation("Se obtiene el listado de Participantes");
            return await dbContext.Participantes.ToListAsync();
        }

        [HttpGet] //Busqueda por ID
        public async Task<ActionResult<GetParticipantesDTO>> GetById(int id)
        {
            logger.LogInformation("Se obtiene Participante por id");

            var participante = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            if (participante == null)
            {
                return NotFound();
            }
            // participante.RifasParticipantes = participante.RifasParticipantes.OrderBy(x => x.Orden).ToList();
            return mapper.Map<GetParticipantesDTO>(participante);
        }

        [HttpPost]
        public async Task<ActionResult> Post(ParticipanteDTO participanteDTO)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == participanteDTO.IdRifa);

            if (existe)
            {
                return BadRequest($"Ya existe esa rifa con el id {participanteDTO.IdRifa}");
            }

            var participante = mapper.Map<Participante>(participanteDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            var DTOparticipante = mapper.Map<ObtenerParticipantesDTO>(participante);
            return CreatedAtRoute("Obtener Participante", new { id = participante.Id }, DTOparticipante);

        }

            [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
        [HttpPut("{id:int}")]

        [HttpDelete]

        [HttpPatch]

    }
}
