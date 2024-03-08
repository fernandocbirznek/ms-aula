using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class TituloSessaoAula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "AulaSessao",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "AulaSessao");
        }
    }
}
