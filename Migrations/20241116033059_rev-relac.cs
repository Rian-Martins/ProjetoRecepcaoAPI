using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class revrelac : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.DropIndex(
                name: "IX_PlanilhaReposicao_AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.DropColumn(
                name: "AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "criacaoplanalunos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "AlunoId1",
                table: "PlanilhaReposicao",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "criacaoplanalunos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "alunosreposicaos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanilhaReposicao_AlunoId1",
                table: "PlanilhaReposicao",
                column: "AlunoId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId1",
                table: "PlanilhaReposicao",
                column: "AlunoId1",
                principalTable: "Alunos",
                principalColumn: "AlunoId");
        }
    }
}
