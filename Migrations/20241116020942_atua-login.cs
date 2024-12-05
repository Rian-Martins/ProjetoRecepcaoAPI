using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class atualogin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "logins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "logins",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "cadastros",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Senha",
                table: "cadastros",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "logins");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "cadastros");

            migrationBuilder.DropColumn(
                name: "Senha",
                table: "cadastros");
        }
    }
}
