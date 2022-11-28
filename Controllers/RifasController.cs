﻿using AutoMapper;
using CasinoBubble.DTOs;
using CasinoBubble.Filtros;
using CasinoBubble;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CasinoBubble.Entidades;


namespace WebApiLoteria.Controllers
{
    [ApiController]
    [Route("api/rifas")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "Administrador")]
    public class RifasController : ControllerBase
    {

        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<RifasController> logger;
        private readonly IMapper mapper;

        public RifasController(ApplicationDbContext context, ILogger<RifasController> logger, IMapper mapper)
        {
            this.dbContext = context;
            this.logger = logger;
            this.mapper = mapper;
        }

        [HttpGet]
        [ServiceFilter(typeof(FiltroPersonalizado))]
        public async Task<List<RifaDTO>> Get()
        {
            logger.LogInformation("Se obtiene el listado de rifas");
            var rifas = await dbContext.Rifas.ToListAsync();
            return mapper.Map<List<RifaDTO>>(rifas);
        }

        [HttpGet("{id:int}", Name = "obtenerRifa")]
        public async Task<ActionResult<ObtenerRifa>> Get(int id)
        {
            var rifa = await dbContext.Rifas.FirstOrDefaultAsync(rifaBD => rifaBD.Id == id);
            if (rifa == null)
            {
                return NotFound();
            }

            return mapper.Map<ObtenerRifa>(rifa);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] RifaCreacionDTO crearRifaDTO)
        {
            var existeRifa = await dbContext.Rifas.AnyAsync(x => x.NombreRifa == crearRifaDTO.NombreRifa);

            if (existeRifa)
            {
                return BadRequest($"Ya existe esa rifa con el nombre {crearRifaDTO.NombreRifa}");
            }

            var rifa = mapper.Map<RifaLoteria>(crearRifaDTO);
            dbContext.Add(rifa);
            await dbContext.SaveChangesAsync();

            var rifaDT0 = mapper.Map<RifaDTO>(rifa);
            return CreatedAtRoute("obtenerRifa", new { id = rifa.Id }, rifaDT0);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(RifaCreacionDTO rifaCreacionDTO, int id)
        {
            var existe = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!existe)
            {
                return NotFound();
            }
            var rifa = mapper.Map<RifaLoteria>(rifaCreacionDTO);
            rifa.Id = id;
            dbContext.Update(rifa);
            await dbContext.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Rifas.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new RifaLoteria()
            {
                Id = id
            });
            logger.LogInformation("Se ha eliminado la rifa");
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}


