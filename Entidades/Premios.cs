namespace CasinoBubble.Entidades
{
    public class Premios
    {
        public int Id { get; set; }
        public string NombrePremio { get; set; }
        
        public int IdRifa { get; set; }
        public RifaLoteria RifaLoteria { get; set; }
    }
}
