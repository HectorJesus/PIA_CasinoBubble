using AutoMapper;
using CasinoBubble.DTOs;
using CasinoBubble.Entidades;
using CasinoBubble.Filtros;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//avr
namespace CasinoBubble.Controllers
{
    [ApiController]
    [Route("api/participantes")]
    public class ParticipantesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<ParticipantesController> logger;
        private readonly IMapper mapper;

        public ParticipantesController(ApplicationDbContext context, ILogger<ParticipantesController> logger, IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpGet("Obtener participantes")]
        [ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<ActionResult<List<Participante>>> GetAll()
        {
            logger.LogInformation("Se obtiene el listado de Participantes");
            return await dbContext.Participantes.ToListAsync();
        }

        [HttpGet("{id:int} Obtener Participantes")] //Busqueda por ID
        public async Task<ActionResult<ObtenerParticipantesDTO>> GetById(int id)
        {
            logger.LogInformation("Se obtiene Participante por id");

            var participante = await dbContext.Participantes.FirstOrDefaultAsync(x => x.Id == id);
            if (participante == null)
            {
                return NotFound();
            }

            return mapper.Map<ObtenerParticipantesDTO>(participante);
        }

        [HttpPost("Crear Participante")]
        //[AllowAnonymous]
        public async Task<ActionResult> Post(CrearParticipanteDTO crearParticipanteDTO)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Nombre == crearParticipanteDTO.Nombre);

            if (existe)
            {
                return BadRequest($"El participante ya existe" + $"{crearParticipanteDTO.Nombre}");
            }

            var participante = mapper.Map<Participante>(crearParticipanteDTO);

            dbContext.Add(participante);
            await dbContext.SaveChangesAsync();
            var DTOparticipante = mapper.Map<ObtenerParticipantesDTO>(participante);
            return NoContent();

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpPut("{id:int} Reemplazar Participante")]
        //[AllowAnonymous]

        public async Task<ActionResult> Put(ParticipanteDTO participanteDTO, int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("El usuario no existe");
            }

            var participante = mapper.Map<Participante>(participanteDTO);
            participante.Id = id;

            dbContext.Update(participante);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpDelete("{id:int} Eliminar Participante")]
        
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await dbContext.Participantes.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound("No se encontro el participante");
            }
            dbContext.Remove(new Participante { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
        [HttpPatch("{id:int} Editar Participante")]
        //[AllowAnonymous]

        public async Task<ActionResult> Patch(int id, JsonPatchDocument<ParticipantePatchDTO> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }
            var participanteDB = await dbContext.Participantes.FirstOrDefaultAsync(X => X.Id == id);

            if (participanteDB == null)
            {
                return NotFound();
            }
            var participanteDTO = mapper.Map<ParticipantePatchDTO>(participanteDB);
            participanteDB.Id = id;
            patchDocument.ApplyTo(participanteDTO);
            var esValid = TryValidateModel(participanteDTO);
            if (!esValid)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(participanteDTO, participanteDB);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}


