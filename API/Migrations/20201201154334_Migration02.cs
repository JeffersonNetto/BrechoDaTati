using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace API.Migrations
{
    public partial class Migration02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Produto",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AddColumn<short>(
                name: "CondicaoId",
                table: "Produto",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<string>(
                name: "Cor",
                table: "Produto",
                type: "varchar(20)",
                unicode: false,
                maxLength: 20,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "MangaId",
                table: "Produto",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Medidas",
                table: "Produto",
                type: "varchar(200)",
                unicode: false,
                maxLength: 200,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<short>(
                name: "ModelagemId",
                table: "Produto",
                type: "smallint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Observacoes",
                table: "Produto",
                type: "varchar(300)",
                unicode: false,
                maxLength: 300,
                nullable: true);

            migrationBuilder.AddColumn<short>(
                name: "TamanhoId",
                table: "Produto",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);

            migrationBuilder.AddColumn<short>(
                name: "TecidoId",
                table: "Produto",
                type: "smallint",
                nullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Marca",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Cliente",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Categoria",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValueSql: "((1))");

            migrationBuilder.CreateTable(
                name: "Condicao",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Condicao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Manga",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manga", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modelagem",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modelagem", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tamanho",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tamanho", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tecido",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataCriacao = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    DataAtualizacao = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tecido", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_CondicaoId",
                table: "Produto",
                column: "CondicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_MangaId",
                table: "Produto",
                column: "MangaId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_ModelagemId",
                table: "Produto",
                column: "ModelagemId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_TamanhoId",
                table: "Produto",
                column: "TamanhoId");

            migrationBuilder.CreateIndex(
                name: "IX_Produto_TecidoId",
                table: "Produto",
                column: "TecidoId");

            migrationBuilder.CreateIndex(
                name: "UQ__Tamanho__008BA9EF2E2004D4",
                table: "Tamanho",
                column: "Descricao",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK__Produto__Condica__6C190EBB",
                table: "Produto",
                column: "CondicaoId",
                principalTable: "Condicao",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Produto__MangaId__787EE5A0",
                table: "Produto",
                column: "MangaId",
                principalTable: "Manga",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Produto__Modelag__797309D9",
                table: "Produto",
                column: "ModelagemId",
                principalTable: "Modelagem",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Produto__Tamanho__5FB337D6",
                table: "Produto",
                column: "TamanhoId",
                principalTable: "Tamanho",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK__Produto__TecidoI__7A672E12",
                table: "Produto",
                column: "TecidoId",
                principalTable: "Tecido",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK__Produto__Condica__6C190EBB",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK__Produto__MangaId__787EE5A0",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK__Produto__Modelag__797309D9",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK__Produto__Tamanho__5FB337D6",
                table: "Produto");

            migrationBuilder.DropForeignKey(
                name: "FK__Produto__TecidoI__7A672E12",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Condicao");

            migrationBuilder.DropTable(
                name: "Manga");

            migrationBuilder.DropTable(
                name: "Modelagem");

            migrationBuilder.DropTable(
                name: "Tamanho");

            migrationBuilder.DropTable(
                name: "Tecido");

            migrationBuilder.DropIndex(
                name: "IX_Produto_CondicaoId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_MangaId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_ModelagemId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_TamanhoId",
                table: "Produto");

            migrationBuilder.DropIndex(
                name: "IX_Produto_TecidoId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "CondicaoId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Cor",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "MangaId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Medidas",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "ModelagemId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "Observacoes",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TamanhoId",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "TecidoId",
                table: "Produto");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Produto",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Marca",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Cliente",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<bool>(
                name: "Ativo",
                table: "Categoria",
                type: "bit",
                nullable: false,
                defaultValueSql: "((1))",
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
