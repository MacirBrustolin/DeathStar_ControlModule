using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DeathStar_Data.Migrations
{
    public partial class InitialTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Naves",
                columns: table => new
                {
                    NaveId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Modelo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Passageiros = table.Column<int>(type: "int", nullable: false),
                    Carga = table.Column<double>(type: "float", nullable: false),
                    Classe = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Naves", x => x.NaveId);
                });

            migrationBuilder.CreateTable(
                name: "Planetas",
                columns: table => new
                {
                    PlanetaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rotacao = table.Column<double>(type: "float", nullable: false),
                    Orbita = table.Column<double>(type: "float", nullable: false),
                    Diametro = table.Column<double>(type: "float", nullable: false),
                    Clima = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Populacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planetas", x => x.PlanetaId);
                });

            migrationBuilder.CreateTable(
                name: "Pilotos",
                columns: table => new
                {
                    PilotoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoNascimento = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlanetasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilotos", x => x.PilotoId);
                    table.ForeignKey(
                        name: "FK_Pilotos_Planetas_PlanetasId",
                        column: x => x.PlanetasId,
                        principalTable: "Planetas",
                        principalColumn: "PlanetaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "HistoricoViagens",
                columns: table => new
                {
                    ViagemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NavesNaveId = table.Column<int>(type: "int", nullable: false),
                    PilotosPilotoId = table.Column<int>(type: "int", nullable: false),
                    DtSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DtChegada = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricoViagens", x => x.ViagemId);
                    table.ForeignKey(
                        name: "FK_HistoricoViagens_Naves_NavesNaveId",
                        column: x => x.NavesNaveId,
                        principalTable: "Naves",
                        principalColumn: "NaveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HistoricoViagens_Pilotos_PilotosPilotoId",
                        column: x => x.PilotosPilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "PilotoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NavePiloto",
                columns: table => new
                {
                    NavesNaveId = table.Column<int>(type: "int", nullable: false),
                    PilotosPilotoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NavePiloto", x => new { x.NavesNaveId, x.PilotosPilotoId });
                    table.ForeignKey(
                        name: "FK_NavePiloto_Naves_NavesNaveId",
                        column: x => x.NavesNaveId,
                        principalTable: "Naves",
                        principalColumn: "NaveId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NavePiloto_Pilotos_PilotosPilotoId",
                        column: x => x.PilotosPilotoId,
                        principalTable: "Pilotos",
                        principalColumn: "PilotoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoViagens_NavesNaveId",
                table: "HistoricoViagens",
                column: "NavesNaveId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricoViagens_PilotosPilotoId",
                table: "HistoricoViagens",
                column: "PilotosPilotoId");

            migrationBuilder.CreateIndex(
                name: "IX_NavePiloto_PilotosPilotoId",
                table: "NavePiloto",
                column: "PilotosPilotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Pilotos_PlanetasId",
                table: "Pilotos",
                column: "PlanetasId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HistoricoViagens");

            migrationBuilder.DropTable(
                name: "NavePiloto");

            migrationBuilder.DropTable(
                name: "Naves");

            migrationBuilder.DropTable(
                name: "Pilotos");

            migrationBuilder.DropTable(
                name: "Planetas");
        }
    }
}
