using CasinoBubble.Entidades;

namespace CasinoBubble.Entidades
{
    public class ParticipanteRifa
    {
        public int RifaId { get; set; }
        public int ParticipanteId { get; set; }
        public int Orden { get; set; }
        public RifaLoteria RifaLoteria { get; set; }
        public Participante Participante { get; set; }
    }
}
