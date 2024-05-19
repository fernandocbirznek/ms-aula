using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class AulaPublicadoAndAulaPosteriorAndAnterior : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AulaAnteriorId",
                table: "Aula",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "AulaPosteriorId",
                table: "Aula",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AulaAnteriorId",
                table: "Aula");

            migrationBuilder.DropColumn(
                name: "AulaPosteriorId",
                table: "Aula");
        }
    }
}
