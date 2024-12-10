using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoRecepcao.Migrations
{
    /// <inheritdoc />
    public partial class retirIdReposic : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "PlanilhaReposicao");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao",
                column: "AlunoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlanilhaReposicao",
                table: "PlanilhaReposicao",
                column: "Id");
        }
    }
}
