using Microsoft.EntityFrameworkCore.Migrations;

namespace XPTO.Migrations
{
    public partial class AdicionandoCaixa : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Usuarios(Id, Nome, NumeroCartao, Senha, Saldo, Role) VALUES(1, 'Admin','0000000000000000', '@@@@@@', 0,'Admin')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Usuarios");
        }
    }
}
