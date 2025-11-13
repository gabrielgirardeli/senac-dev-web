using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MeuCorre.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "DataAtualizacao", "DataCriacao", "DataNascimento", "Email", "Nome", "Senha" },
                values: new object[] { new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b"), true, null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(1985, 7, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "weltoncastoldi@hotmail.com", "Welton Castoldi", "123456" });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Ativo", "Cor", "DataAtualizacao", "DataCriacao", "Descricao", "Icone", "Nome", "TipoDaTransacao", "UsuarioId" },
                values: new object[,]
                {
                    { new Guid("0a1b2c3d-4e5f-4678-9a0b-1c2d3e4f5a6b"), true, "#E6F8E6", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Aplicações financeiras e rendimentos (ações, fundos)", "📈", "Investimentos", 1, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("9f1e2d3c-4b5a-6c7d-8e9f-0a1b2c3d4e5f"), true, "#BEE3F8", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesas relacionadas à casa e moradia (aluguel, condomínio, contas)", "🏠", "Moradia", 2, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"), true, "#DFF7E0", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gastos com alimentação (supermercado, restaurantes)", "🍔", "Alimentação", 2, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("b7c6d5e4-f3a2-41b0-9c8d-7e6f5a4b3c2d"), true, "#FFD1D1", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesas médicas e de saúde (consultas, medicamentos)", "💊", "Saúde", 2, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("c0d1e2f3-0415-4a6b-8c7d-9e8f7a6b5c4d"), true, "#FFF5BA", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Gastos com transporte (combustível, ônibus, manutenção)", "🚗", "Transporte", 2, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("d4e5f6a7-b8c9-40d1-8e2f-3a4b5c6d7e8f"), true, "#E8D8FF", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Despesas com lazer e entretenimento (cinema, viagens)", "🎮", "Lazer", 2, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("e6f7a8b9-c0d1-4e2f-9a3b-5c6d7e8f9a0b"), true, "#D1F7FF", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rendimento principal do trabalho (salário)", "💼", "Salário", 1, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") },
                    { new Guid("f1a2b3c4-d5e6-4789-8b0c-2d3e4f5a6b7c"), true, "#F0F0F0", null, new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Outras receitas diversas não classificadas", "📦", "Outras", 1, new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("0a1b2c3d-4e5f-4678-9a0b-1c2d3e4f5a6b"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("9f1e2d3c-4b5a-6c7d-8e9f-0a1b2c3d4e5f"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("a1b2c3d4-e5f6-47a8-9b0c-1d2e3f4a5b6c"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("b7c6d5e4-f3a2-41b0-9c8d-7e6f5a4b3c2d"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("c0d1e2f3-0415-4a6b-8c7d-9e8f7a6b5c4d"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("d4e5f6a7-b8c9-40d1-8e2f-3a4b5c6d7e8f"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("e6f7a8b9-c0d1-4e2f-9a3b-5c6d7e8f9a0b"));

            migrationBuilder.DeleteData(
                table: "Categorias",
                keyColumn: "Id",
                keyValue: new Guid("f1a2b3c4-d5e6-4789-8b0c-2d3e4f5a6b7c"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("da3b9f4c-8e6a-4a4f-9e6b-1c2d3e4f5a6b"));
        }
    }
}
