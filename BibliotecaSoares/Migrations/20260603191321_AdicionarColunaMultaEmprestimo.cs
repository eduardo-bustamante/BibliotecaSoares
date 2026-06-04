using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BibliotecaSoares.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarColunaMultaEmprestimo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ValorMultaPaga",
                table: "Emprestimos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValorMultaPaga",
                table: "Emprestimos");
        }
    }
}
