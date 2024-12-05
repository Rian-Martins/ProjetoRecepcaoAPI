using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class inicialAlunos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "PlanilhaReposicao",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "AlunoId1",
                table: "PlanilhaReposicao",
                type: "uuid",
                nullable: true);

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "alunosreposicaos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Horario = table.Column<string>(type: "text", nullable: false),
                    DiaSemana = table.Column<DateOnly>(type: "date", nullable: false),
                    Observacoes = table.Column<string>(type: "text", nullable: false),
                    Reagendamento = table.Column<bool>(type: "boolean", nullable: false),
                    PlanilhaReposicaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_alunosreposicaos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_alunosreposicaos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_alunosreposicaos_PlanilhaReposicao_PlanilhaReposicaoId",
                        column: x => x.PlanilhaReposicaoId,
                        principalTable: "PlanilhaReposicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cadastros",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Funcao = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cadastros", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "criacaoplanalunos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    AlunoId = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    DataNovaMarcacao = table.Column<DateOnly>(type: "date", nullable: false),
                    Horario = table.Column<string>(type: "text", nullable: false),
                    PlanilhaReposicaoId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_criacaoplanalunos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_criacaoplanalunos_Alunos_AlunoId",
                        column: x => x.AlunoId,
                        principalTable: "Alunos",
                        principalColumn: "AlunoId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_criacaoplanalunos_PlanilhaReposicao_PlanilhaReposicaoId",
                        column: x => x.PlanilhaReposicaoId,
                        principalTable: "PlanilhaReposicao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "logins",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Nome = table.Column<string>(type: "text", nullable: false),
                    Funcao = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_logins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PlanilhaReposicao_AlunoId",
                table: "PlanilhaReposicao",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanilhaReposicao_AlunoId1",
                table: "PlanilhaReposicao",
                column: "AlunoId1");

            migrationBuilder.CreateIndex(
                name: "IX_alunosreposicaos_AlunoId",
                table: "alunosreposicaos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_alunosreposicaos_PlanilhaReposicaoId",
                table: "alunosreposicaos",
                column: "PlanilhaReposicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_criacaoplanalunos_AlunoId",
                table: "criacaoplanalunos",
                column: "AlunoId");

            migrationBuilder.CreateIndex(
                name: "IX_criacaoplanalunos_PlanilhaReposicaoId",
                table: "criacaoplanalunos",
                column: "PlanilhaReposicaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId",
                table: "PlanilhaReposicao",
                column: "AlunoId",
                principalTable: "Alunos",
                principalColumn: "AlunoId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId1",
                table: "PlanilhaReposicao",
                column: "AlunoId1",
                principalTable: "Alunos",
                principalColumn: "AlunoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId",
                table: "PlanilhaReposicao");

            migrationBuilder.DropForeignKey(
                name: "FK_PlanilhaReposicao_Alunos_AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.DropTable(
                name: "alunosreposicaos");

            migrationBuilder.DropTable(
                name: "cadastros");

            migrationBuilder.DropTable(
                name: "criacaoplanalunos");

            migrationBuilder.DropTable(
                name: "logins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao");

            migrationBuilder.DropIndex(
                name: "IX_PlanilhaReposicao_AlunoId",
                table: "PlanilhaReposicao");

            migrationBuilder.DropIndex(
                name: "IX_PlanilhaReposicao_AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlanilhaReposicao");

            migrationBuilder.DropColumn(
                name: "AlunoId1",
                table: "PlanilhaReposicao");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao",
                column: "AlunoId");
        }
    }
}
