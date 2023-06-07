using Microsoft.EntityFrameworkCore.Migrations;

namespace XPTO.Migrations
{
    public partial class CriandoCaizasNoBD : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CaixaEletronicos(Id, Notas100, Notas50, Notas20, Notas10) VALUES(1, 150, 200, 200, 150)");
            migrationBuilder.Sql("INSERT INTO CaixaEletronicos(Id, Notas100, Notas50, Notas20, Notas10) VALUES(2, 120, 150, 140, 100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM CaixaEletronicos");
        }
    }
}
