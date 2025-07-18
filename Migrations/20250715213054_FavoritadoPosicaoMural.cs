using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ms_aula.Migrations
{
    /// <inheritdoc />
    public partial class FavoritadoPosicaoMural : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "MuralPosicaoX",
                table: "AulaSessaoFavoritada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "MuralPosicaoY",
                table: "AulaSessaoFavoritada",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MuralPosicaoX",
                table: "AulaSessaoFavoritada");

            migrationBuilder.DropColumn(
                name: "MuralPosicaoY",
                table: "AulaSessaoFavoritada");
        }
    }
}
