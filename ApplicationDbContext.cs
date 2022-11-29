using CasinoBubble.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


namespace CasinoBubble
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ParticipanteRifa>()
                .HasKey(al => new { al.RifaId, al.ParticipanteId });
        }
        public DbSet<RifaLoteria> Rifas { get; set; }
        public DbSet<Participante> Participantes { get; set; }

        public DbSet<ParticipanteRifa> ParticipanteRifa { get; set; }
    }
}
