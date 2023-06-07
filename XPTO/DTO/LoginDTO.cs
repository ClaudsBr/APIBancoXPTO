using System.ComponentModel.DataAnnotations;

namespace XPTO.DTO
{
    public class LoginDTO
    {
        [MaxLength(16, ErrorMessage ="O numero do cartão deve conter16 dígitos")]
        [MinLength(16, ErrorMessage ="O numero do cartão deve conter16 dígitos")]
        public string NumeroCartao{get;set;}
        
        [Required(ErrorMessage ="Senha obrigatória")]
        [MinLength(6)]
        public string Senha {get;set;}
    }
}