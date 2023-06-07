using System.ComponentModel.DataAnnotations;

namespace XPTO.DTO
{
    public class UsuarioDTO
    {
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
    }
}