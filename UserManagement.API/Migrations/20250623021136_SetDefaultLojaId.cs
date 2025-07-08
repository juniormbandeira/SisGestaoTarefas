using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class SetDefaultLojaId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LojaId",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateTable(
                name: "Lojas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Descricao = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lojas", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Lojas",
                columns: new[] { "Id", "Descricao", "Nome" },
                values: new object[,]
                {
                    { 1, "Loja principal", "Matriz" },
                    { 2, "Primeira filial", "Filial A" }
                });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "LojaId" },
                values: new object[] { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1562), new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "LojaId" },
                values: new object[] { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1582), new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "LojaId" },
                values: new object[] { new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1587), 1 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "LojaId" },
                values: new object[] { new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1595), new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), 2 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "LojaId" },
                values: new object[] { new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1598), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc), 2 });

            migrationBuilder.CreateIndex(
                name: "IX_Tarefas_LojaId",
                table: "Tarefas",
                column: "LojaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tarefas_Lojas_LojaId",
                table: "Tarefas",
                column: "LojaId",
                principalTable: "Lojas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tarefas_Lojas_LojaId",
                table: "Tarefas");

            migrationBuilder.DropTable(
                name: "Lojas");

            migrationBuilder.DropIndex(
                name: "IX_Tarefas_LojaId",
                table: "Tarefas");

            migrationBuilder.DropColumn(
                name: "LojaId",
                table: "Tarefas");

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1352), new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1361));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1374), new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1379), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
