using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class revisaoconcertopropsalunoReposicao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                oldClrType: typeof(DateOnly),
                oldType: "date");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateOnly>(
                name: "DiaSemana",
                table: "alunosreposicaos",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }
    }
}
