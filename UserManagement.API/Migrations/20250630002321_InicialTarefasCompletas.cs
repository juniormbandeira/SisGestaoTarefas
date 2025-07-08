using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InicialTarefasCompletas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 30, 0, 23, 18, 118, DateTimeKind.Utc).AddTicks(1060), new DateTime(2025, 7, 2, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 30, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 30, 0, 23, 18, 118, DateTimeKind.Utc).AddTicks(1074), new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 6, 30, 0, 23, 18, 118, DateTimeKind.Utc).AddTicks(1080));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 30, 0, 23, 18, 118, DateTimeKind.Utc).AddTicks(1088), new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 7, 10, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 30, 0, 23, 18, 118, DateTimeKind.Utc).AddTicks(1092), new DateTime(2025, 7, 15, 0, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1562), new DateTime(2025, 6, 25, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1582), new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1587));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 24, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1595), new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 7, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 23, 2, 11, 36, 260, DateTimeKind.Utc).AddTicks(1598), new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
