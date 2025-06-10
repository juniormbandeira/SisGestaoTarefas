using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace UserManagement.AP.Migrations
{
    /// <inheritdoc />
    public partial class SeedTarefasDataCompletas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tarefas",
                columns: new[] { "Id", "CriadorId", "DataAgendamento", "DataConclusao", "DataCriacao", "DataLimiteFinalizacao", "Descricao", "EvidenciaUrl", "Nome", "ResponsavelId", "SetorId", "Status" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(513), new DateTime(2025, 5, 28, 0, 0, 0, 0, DateTimeKind.Utc), "Verificar todos os endpoints e exemplos da documentação da API de usuários.", null, "Revisar Documentação API V1", 2, 1, "Agendada" },
                    { 2, 1, new DateTime(2025, 5, 26, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(527), new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Compilar dados para o relatório de progresso da equipe de desenvolvimento.", null, "Preparar Relatório Semanal de Progresso", 2, 1, "EmAndamento" },
                    { 3, 1, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), null, new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(532), new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Definir e estimar tarefas para a próxima sprint do projeto X.", null, "Planejamento da Sprint de Junho", 1, 1, "Agendada" },
                    { 4, 1, new DateTime(2025, 5, 27, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(539), new DateTime(2025, 5, 29, 0, 0, 0, 0, DateTimeKind.Utc), "Realizar entrevistas com os candidatos finalistas para a vaga de Analista de RH.", null, "Entrevistas Candidatos Analista RH", 3, 2, "Agendada" },
                    { 5, 1, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Utc), null, new DateTime(2025, 5, 26, 2, 36, 50, 156, DateTimeKind.Utc).AddTicks(542), new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), "Preparar e apresentar os resultados financeiros do último trimestre.", null, "Apresentar Resultados Trimestrais", 1, 3, "Agendada" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tarefas",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
