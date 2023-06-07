using System.Collections.Generic;

namespace XPTO.Models
{
    public class CaixaEletronico
    {
        public int Id {get;set;}
        public int Notas100 {get;set;}
        public int Notas50 {get;set;}
        public int Notas20 {get;set;}
        public int Notas10 {get;set;}

        public IDictionary<int, int> Saque(int[] notas, int valor){
            IDictionary<int, int> totalNotas
            = new Dictionary<int, int>();
            string impressao = "";
            
            foreach(int nota in notas){
                int qtdNota = valor/nota;
                valor -= qtdNota * nota;
                totalNotas.Add(nota, qtdNota);
                impressao += qtdNota+" nota(s) de "+nota+"\n"; 
            }
            
            return totalNotas;
        }

       
    }
}