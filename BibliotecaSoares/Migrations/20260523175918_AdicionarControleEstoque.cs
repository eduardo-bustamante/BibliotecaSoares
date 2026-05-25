using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaSoares.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarControleEstoque : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Quantidade",
                table: "Livros",
                newName: "QuantidadeTotal");

            migrationBuilder.AddColumn<int>(
                name: "QuantidadeEmprestada",
                table: "Livros",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "QuantidadeEmprestada",
                table: "Livros");

            migrationBuilder.RenameColumn(
                name: "QuantidadeTotal",
                table: "Livros",
                newName: "Quantidade");
        }
    }
}
