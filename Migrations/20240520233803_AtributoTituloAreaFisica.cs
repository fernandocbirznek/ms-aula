using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class AtributoTituloAreaFisica : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Titulo",
                table: "AreaFisica",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Titulo",
                table: "AreaFisica");
        }
    }
}
