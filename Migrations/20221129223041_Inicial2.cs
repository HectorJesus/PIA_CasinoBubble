using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CasinoBubble.Migrations
{
    public partial class Inicial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Orden",
                table: "ParticipanteRifa",
                newName: "boletoId");

            migrationBuilder.CreateTable(
                name: "Premios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombrePremio = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdRifa = table.Column<int>(type: "int", nullable: false),
                    RifaLoteriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Premios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Premios_Rifas_RifaLoteriaId",
                        column: x => x.RifaLoteriaId,
                        principalTable: "Rifas",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Premios_RifaLoteriaId",
                table: "Premios",
                column: "RifaLoteriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Premios");

            migrationBuilder.RenameColumn(
                name: "boletoId",
                table: "ParticipanteRifa",
                newName: "Orden");
        }
    }
}
