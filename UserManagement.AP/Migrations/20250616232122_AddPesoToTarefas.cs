using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserManagement.AP.Migrations
{
    /// <inheritdoc />
    public partial class AddPesoToTarefas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Peso",
                table: "Tarefas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "Peso" },
                values: new object[] { new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1330), new DateTime(2025, 6, 18, 0, 0, 0, 0, DateTimeKind.Utc), 3 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "Peso" },
                values: new object[] { new DateTime(2025, 6, 16, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1352), new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), 2 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "DataCriacao", "Peso" },
                values: new object[] { new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1361), 5 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "Peso" },
                values: new object[] { new DateTime(2025, 6, 17, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1374), new DateTime(2025, 6, 19, 0, 0, 0, 0, DateTimeKind.Utc), 1 });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao", "Peso" },
                values: new object[] { new DateTime(2025, 6, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 6, 16, 23, 21, 21, 590, DateTimeKind.Utc).AddTicks(1379), new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Peso",
                table: "Tarefas");

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(513), new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(527), new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3,
                column: "DataCriacao",
                value: new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(532));

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(539), new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "DataAgendamento", "DataCriacao", "DataLimiteFinalizacao" },
                values: new object[] { new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(542), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc) });
        }
    }
}
