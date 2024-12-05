using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class atualizdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_alunosreposicaos_Alunos_AlunoId",
                table: "alunosreposicaos");

            migrationBuilder.DropForeignKey(
                name: "FK_alunosreposicaos_PlanilhaReposicao_PlanilhaReposicaoId",
                table: "alunosreposicaos");

            migrationBuilder.DropForeignKey(
                name: "FK_criacaoplanalunos_Alunos_AlunoId",
                table: "criacaoplanalunos");

            migrationBuilder.DropForeignKey(
                name: "FK_criacaoplanalunos_PlanilhaReposicao_PlanilhaReposicaoId",
                table: "criacaoplanalunos");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId",
                table: "PlanilhaReposicao");

            migrationBuilder.DropIndex(
                name: "IX_PlanilhaReposicao_AlunoId",
                table: "PlanilhaReposicao");

            migrationBuilder.DropIndex(
                name: "IX_criacaoplanalunos_AlunoId",
                table: "criacaoplanalunos");

            migrationBuilder.DropIndex(
                name: "IX_criacaoplanalunos_PlanilhaReposicaoId",
                table: "criacaoplanalunos");

            migrationBuilder.DropIndex(
                name: "IX_alunosreposicaos_AlunoId",
                table: "alunosreposicaos");

            migrationBuilder.DropIndex(
                name: "IX_alunosreposicaos_PlanilhaReposicaoId",
                table: "alunosreposicaos");

            migrationBuilder.DropColumn(
                name: "PlanilhaReposicaoId",
                table: "criacaoplanalunos");

            migrationBuilder.DropColumn(
                name: "PlanilhaReposicaoId",
                table: "alunosreposicaos");

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "PlanilhaReposicao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "PlanilhaReposicao",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "PlanilhaReposicao",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "logins",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Funcao",
                table: "logins",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "logins",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "criacaoplanalunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "criacaoplanalunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<Guid>(
                name: "AlunoId",
                table: "criacaoplanalunos",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<bool>(
                name: "Reagendamento",
                table: "alunosreposicaos",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "alunosreposicaos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "alunosreposicaos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "alunosreposicaos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "alunosreposicaos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Data",
                table: "Alunos",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "date",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "PlanilhaReposicao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "PlanilhaReposicao",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Data",
                table: "PlanilhaReposicao",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "logins",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Funcao",
                table: "logins",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Descricao",
                table: "logins",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "criacaoplanalunos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "criacaoplanalunos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "AlunoId",
                table: "criacaoplanalunos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanilhaReposicaoId",
                table: "criacaoplanalunos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<bool>(
                name: "Reagendamento",
                table: "alunosreposicaos",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Observacoes",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "alunosreposicaos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "PlanilhaReposicaoId",
                table: "alunosreposicaos",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "Horario",
                table: "Alunos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DiaSemana",
                table: "Alunos",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "Data",
                table: "Alunos",
                type: "date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PlanilhaReposicao_AlunoId",
                table: "PlanilhaReposicao",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_criacaoplanalunos_AlunoId",
                table: "criacaoplanalunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_criacaoplanalunos_PlanilhaReposicaoId",
                table: "criacaoplanalunos",
                column: "PlanilhaReposicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_alunosreposicaos_AlunoId",
                table: "alunosreposicaos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_alunosreposicaos_PlanilhaReposicaoId",
                table: "alunosreposicaos",
                column: "PlanilhaReposicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_alunosreposicaos_Alunos_AlunoId",
                table: "alunosreposicaos",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "AlunoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_alunosreposicaos_PlanilhaReposicao_PlanilhaReposicaoId",
                table: "alunosreposicaos",
                column: "PlanilhaReposicaoId",
                principalTable: "PlanilhaReposicao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_criacaoplanalunos_Alunos_AlunoId",
                table: "criacaoplanalunos",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "AlunoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_criacaoplanalunos_PlanilhaReposicao_PlanilhaReposicaoId",
                table: "criacaoplanalunos",
                column: "PlanilhaReposicaoId",
                principalTable: "PlanilhaReposicao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId",
                table: "PlanilhaReposicao",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "AlunoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
