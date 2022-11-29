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
    [ApiController]
    [Route("premiossss")]
    [AllowAnonymous]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "EsAdministrador")]
    public class PremiosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        
        public PremiosController(ApplicationDbContext context, IMapper mapper)
        {
            this.dbContext = context;
            this.mapper = mapper;
        }

        [HttpGet("{id:int} obtenerPremio")]
        public async Task<ActionResult<List<ObtenerPremioDTO>>> GetById(int id)
        {
            var premio = await dbContext.Premios.Where(premioDB => premioDB.IdRifa == id).ToListAsync();

            if (premio == null)
            {
                return NotFound();
            }

            return mapper.Map<List<ObtenerPremioDTO>>(premio);
        }
        [HttpPost("postearPremio")]
        public async Task<ActionResult> Post(int rifaId, PremioDTO premioDtO)
        {

            var existeRifa = await dbContext.Rifas.AnyAsync(vetDB => vetDB.Id == rifaId);
            if (!existeRifa)
            {
                return NotFound();
            }

            var premio = mapper.Map<Premios>(premioDtO);

            premio.IdRifa = rifaId;
            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();

            var premioDTO = mapper.Map<ObtenerPremioDTO>(premio);

            //return CreatedAtRoute("obtenerPremio", new { id = premio.Id, rifaId = rifaId }, premioDTO);
            return Ok();
        }

        [HttpPut("{id:int} modificsrPremio")]
        public async Task<ActionResult> Put(int rifaId, int id, PremioDTO premioCreacionDTO)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(vetDB => vetDB.Id == rifaId);
            if (!existeRifa)
            {
                return NotFound();
            }

            var existePremio = await dbContext.Premios.AnyAsync(premioDB => premioDB.Id == id);
            if (!existePremio)
            {
                return NotFound();
            }
            var premio = mapper.Map<Premios>(premioCreacionDTO);
            premio.Id = id;
            premio.IdRifa = rifaId;
            dbContext.Update(premio);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
