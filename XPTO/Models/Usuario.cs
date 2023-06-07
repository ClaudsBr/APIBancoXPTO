using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace XPTO.Models
{
    public class Usuario
    {
        [Key]
        public int Id {get;set;}
        [Required]
        [MaxLength(100)]
        [MinLength(3, ErrorMessage ="O nome do Cliente deve conter no minimo 3 caracteres")]
        public string Nome {get;set;}
        
        [MaxLength(16, ErrorMessage ="O numero do cartão deve conter 16 dígitos")]
        [MinLength(16, ErrorMessage ="O numero do cartão deve conter 16 dígitos")]
        public string NumeroCartao{get;set;}
        
        [Required(ErrorMessage ="Senha obrigatória")]
        [MinLength(6)]
        public string Senha{get;set;}
        [Required(ErrorMessage ="É necessario informar o saldo do cliente")]        
        public int Saldo {get;set;}
        public string Role{get;set;}
        

        public Usuario(string nome, string numeroCartao, string senha, int saldo){
            Nome = nome;
            NumeroCartao = numeroCartao;
            Senha = senha;
            Saldo = saldo;
            Role = "Cliente";
        }

        public bool CartaoValido(string number)
        {
            string primeiros4Digitos = number.Substring(0,4);
            int numero = Int32.Parse(primeiros4Digitos);
            if(numero < 1895 || numero > 3567){
                return false;
            }

            if(number.Length== 16){
                if(Regex.IsMatch(number, @"^[0-9]+$"))
                {
                    return true;
                }else 
                {
                    return false;
                }
                
            }
            return false;           
      
        }

        

        public string Extrato(){            
            return "=========== BANCO XPTO =========== "+"\n\n-------- Extrato da Conta --------- "+
                    "\n\nID do Cliente: 0000000"+Id+
                    "\nNome: "+ Nome+"\nNumero do cartão: "+NumeroCartao+
                    "\nData: "+DateTime.Now+
                    "\nSaldo Atual: R$ "+Saldo+",00\n\n-----------------------------------\n"+
                    
                    "-----------------------------------\n\nObrigado e volte sempre ao Banco XPTO!";
        }
        
    }
    
    

    

}