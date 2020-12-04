using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migration01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cliente",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nome = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Sobrenome = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Cpf = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Senha = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Celular = table.Column<string>(type: "char(11)", unicode: false, fixedLength: true, maxLength: 11, nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataNascimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cliente", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Marca",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marca", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "(newid())"),
                    Nome = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    Descricao = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: true),
                    MarcaId = table.Column<short>(type: "smallint", nullable: true),
                    CategoriaId = table.Column<short>(type: "smallint", nullable: true),
                    ValorCompra = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    ValorVenda = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValueSql: "((1))"),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produto_Categoria",
                        column: x => x.CategoriaId,
                        principalTable: "Categoria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Produto_Marca",
                        column: x => x.MarcaId,
                        principalTable: "Marca",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CategoriaId",
                table: "Produto",
                column: "CategoriaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_MarcaId",
                table: "Produto",
                column: "MarcaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cliente");

            migrationBuilder.DropTable(
                name: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropTable(
                name: "Marca");
        }
    }
}
