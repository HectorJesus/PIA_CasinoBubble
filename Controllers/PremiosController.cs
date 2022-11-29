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
        public async Task<ActionResult<List<ObtenerPremioDTO>>> GetById(int idRifa)
        {
            var premio = await dbContext.Premios.Where(premioDB => premioDB.IdRifa == idRifa).ToListAsync();

            if (premio == null)
            {
                return NotFound();
            }

            return mapper.Map<List<ObtenerPremioDTO>>(premio);
        }
        [HttpPost("postearPremio")]
        public async Task<ActionResult> Post(int idRifa, PremioDTO premioDtO)
        {

            var existeRifa = await dbContext.Rifas.AnyAsync(premioDB => premioDB.Id == idRifa);
            if (!existeRifa)
            {
                return NotFound();
            }

            var premio = mapper.Map<Premios>(premioDtO);

            premio.IdRifa = idRifa;
            dbContext.Add(premio);
            await dbContext.SaveChangesAsync();

            var premioDTO = mapper.Map<ObtenerPremioDTO>(premio);

           
            return Ok();
        }

        [HttpPut("{id:int} modificsrPremio")]
        public async Task<ActionResult> Put(int idRifa, int id, PremioDTO premioDtO)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(premioDB => premioDB.Id == idRifa);
            if (!existeRifa)
            {
                return NotFound();
            }

            var existePremio = await dbContext.Premios.AnyAsync(premioDB => premioDB.Id == id);
            if (!existePremio)
            {
                return NotFound();
            }
            var premio = mapper.Map<Premios>(premioDtO);
            premio.Id = id;

            premio.IdRifa = idRifa;
            dbContext.Update(premio);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
