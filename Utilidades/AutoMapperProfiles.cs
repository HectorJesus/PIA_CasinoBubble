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
            //CreateMap<Rifa, RifaDTOConParticipantes>()
            //    .ForMember(rifaDTO => rifaDTO.Participantes, opciones => opciones.MapFrom(MapRifaDTOParticipante));
            //CreateMap<CrearParticipanteDTO, Participante>()
            //    .ForMember(participante => participante.RifasParticipantes, opciones => opciones.MapFrom(MapRifaParticipante));

            CreateMap<Participante, ParticipanteDTO>();
            //CreateMap<Participante, ParticipantesDTOConRifas>()
            //    .ForMember(participanteDTO => participanteDTO.Rifas, opciones => opciones.MapFrom(MapParticipanteDTORifas));
            CreateMap<ParticipantePatchDTO, Participante>().ReverseMap();

            CreateMap<CrearParticipanteDTO, RifaLoteria>();
            CreateMap<CrearParticipanteDTO, Participante>();
            CreateMap<ParticipanteDTO, RifaLoteria>();
            CreateMap<ParticipanteDTO, Participante>();
            CreateMap<Participante, ObtenerParticipantesDTO>();
            CreateMap<RifaLoteria, ObtenerRifa>();
        }
    }
}
