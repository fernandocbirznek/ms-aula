using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class AtributosAreaFisica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Aplicacao",
                table: "AreaFisica",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<List<string>>(
                name: "DivisaoMany",
                table: "AreaFisica",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Aplicacao",
                table: "AreaFisica");

            migrationBuilder.DropColumn(
                name: "DivisaoMany",
                table: "AreaFisica");
        }
    }
}
