using AutoMapper;
using CasinoBubble.DTOs;
using CasinoBubble.Entidades;
using CasinoBubble.Filtros;
using CasinoBubble;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CasinoBubble.Controllers
{
    [ApiController]
    [Route("api/numerosLoteria")]
    public class NumLoteriaController : ControllerBase
    {

        private ApplicationDbContext dbContext;
        public NumLoteriaController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        //[HttpGet]
        //public async Task<List> Get()
        //{

        //}
    }
}
