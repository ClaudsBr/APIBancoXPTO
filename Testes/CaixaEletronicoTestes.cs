using System;
using System.Collections.Generic;
using System.Linq;
using XPTO.Data;
using XPTO.Models;
using Xunit;

namespace Testes
{
    public class CaixaEletronicoTestes
    {
        //Arrange
        
        private CaixaEletronico caixa = new CaixaEletronico();
        private int[] notas = {100,50,20,10};
        

        [Fact]
        public void TestandoOValor30()
        {  
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 30);
            //Assert
            Assert.Equal(0, resultado[100]);
            Assert.Equal(0, resultado[50]);
            Assert.Equal(1,resultado[20]);
            Assert.Equal(1,resultado[10]);            
            
        }

        [Fact]
        public void TestandoOValor80()
        {
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 80);
            //Assert
            Assert.Equal(0, resultado[100]);
            Assert.Equal(1,resultado[50]);
            Assert.Equal(1,resultado[20]);
            Assert.Equal(1,resultado[10]);
        }

        [Fact]        
        public void TestandoOValor10()
        {
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 10);
            //Assert
            Assert.Equal(0, resultado[100]);
            Assert.Equal(0,resultado[50]);
            Assert.Equal(0,resultado[20]);
            Assert.Equal(1,resultado[10]);
        }

        [Fact]
        public void TestandoOValor100()
        {
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 100);
            //Assert
            Assert.Equal(1, resultado[100]);
            Assert.Equal(0,resultado[50]);
            Assert.Equal(0,resultado[20]);
            Assert.Equal(0,resultado[10]);
        }
        [Fact]
        public void TestandoOValor180()
        {
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 180);
            //Assert
            Assert.Equal(1, resultado[100]);
            Assert.Equal(1,resultado[50]);
            Assert.Equal(1,resultado[20]);
            Assert.Equal(1,resultado[10]);
        }

        [Fact]
        public void TestandoOValorZero()
        {
            //Act           
            IDictionary<int, int> resultado = caixa.Saque(notas, 0);
            //Assert
            Assert.Equal(0, resultado[100]);
            Assert.Equal(0,resultado[50]);
            Assert.Equal(0,resultado[20]);
            Assert.Equal(0,resultado[10]);
        }
        
    }
}
