
using XPTO.Models;
using Xunit;

namespace Testes
{
    public class UsuarioTestes
    {
        //Arrange
        private Usuario user = new Usuario("Pedro", "1234567890012345","111111", 300);
        
        [Fact]
        public void TestandoCartaoComNumeroInvalido(){

            //Act
            var resultado = user.CartaoValido(user.NumeroCartao);
            var expected = false;
            //Assert
            Assert.Equal(expected, resultado);

        }
        [Fact]
        public void TestandoCartaoComNumeroValido(){
            var resultado = user.CartaoValido("3567567890123456");
            var expected = true;
            //Assert
            Assert.Equal(expected, resultado);
        }

        [Fact]
        public void TestandoExtrato(){
            var user = new Usuario("Anderson", "3333444455556666", "123456", 500);
            string dados = user.Extrato();
            Assert.Contains("=========== BANCO XPTO =========== ", dados);
        } 

    }
}