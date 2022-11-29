using AutoMapper;
using CasinoBubble.DTOs;
using CasinoBubble.Entidades;

namespace CasinoBubble.Utilidades
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<RifaCreacionDTO, RifaLoteria>();
            CreateMap<RifaLoteria, RifaDTO>();
            CreateMap<Participante, ParticipanteDTO>();
            CreateMap<ParticipantePatchDTO, Participante>().ReverseMap();

            CreateMap<CrearParticipanteDTO, RifaLoteria>();
            CreateMap<CrearParticipanteDTO, Participante>();
            CreateMap<ParticipanteDTO, RifaLoteria>();
            CreateMap<ParticipanteDTO, Participante>();
            CreateMap<Participante, ObtenerParticipantesDTO>();
            CreateMap<RifaLoteria, ObtenerRifa>();

            CreateMap<PremioDTO, Premios>();
            CreateMap<Premios, ObtenerPremioDTO>();

        }
    }
}
