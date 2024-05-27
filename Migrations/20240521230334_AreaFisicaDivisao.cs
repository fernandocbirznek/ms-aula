using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class AreaFisicaDivisao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DivisaoMany",
                table: "AreaFisica");

            migrationBuilder.CreateTable(
                name: "AreaFisicaDivisao",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Titulo = table.Column<string>(type: "text", nullable: true),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Foto = table.Column<byte[]>(type: "bytea", nullable: true),
                    AreaFisicaId = table.Column<long>(type: "bigint", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DataAtualizacao = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AreaFisicaDivisao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AreaFisicaDivisao_AreaFisica_AreaFisicaId",
                        column: x => x.AreaFisicaId,
                        principalTable: "AreaFisica",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AreaFisicaDivisao_AreaFisicaId",
                table: "AreaFisicaDivisao",
                column: "AreaFisicaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AreaFisicaDivisao");

            migrationBuilder.AddColumn<List<string>>(
                name: "DivisaoMany",
                table: "AreaFisica",
                type: "text[]",
                nullable: true);
        }
    }
}
