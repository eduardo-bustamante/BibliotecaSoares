using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaSoares.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCapaLivro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoCapa",
                table: "Livros",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoCapa",
                table: "Livros");
        }
    }
}
