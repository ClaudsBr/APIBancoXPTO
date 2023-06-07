using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using XPTO.Data;
using XPTO.DTO;
using XPTO.Models;

namespace XPTO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosController: ControllerBase
    {
        private readonly ApplicationDbContext database;
        public UsuariosController(ApplicationDbContext database){
            this.database = database;

        }

        [Authorize(Roles="Admin")]
        [HttpGet]
        public IActionResult ListarUsuarios(){
            var usuarios = database.Usuarios.ToList();
            return Ok(usuarios);
        }
        
        [HttpPost("registro")]
        public IActionResult RegistrarUsuario([FromBody]Usuario usuario){
            
            
            try{
                if(usuario.CartaoValido(usuario.NumeroCartao)==false){
                    Response.StatusCode = 401;
                    return new ObjectResult("Numero de Cartão inválido");
                }                
                database.Add(usuario);
                database.SaveChanges();
                Response.StatusCode = 201;
                return new ObjectResult("Usuario Criado com sucesso");

            }catch(Exception e){
                Response.StatusCode = 401;
                return new ObjectResult("Operação Invalida");
            }            
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginDTO login){
            try{
                Usuario usuario = database.Usuarios.First(u=>u.NumeroCartao.Equals(login.NumeroCartao));
                if(usuario != null){
                    if(usuario.Senha.Equals(login.Senha)){
                        string chaveDeSeguranca = "GFTBrasil2022";
                        var chaveSimetrica = new SymmetricSecurityKey(Encoding.UTF32.GetBytes(chaveDeSeguranca));
                        var credencialAcesso = new SigningCredentials(chaveSimetrica, SecurityAlgorithms.HmacSha256Signature);
                        
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Role, usuario.Role));


                        var JWT = new JwtSecurityToken(
                            issuer: "Claudio",
                            expires: DateTime.Now.AddMinutes(30),
                            audience: "Cliente",
                            signingCredentials: credencialAcesso,
                            claims: claims
                        );
                        var token = new JwtSecurityTokenHandler().WriteToken(JWT);
                        return Ok(token);
                    }else{
                        Response.StatusCode = 401;
                        return new ObjectResult("Senha Incorreta");
                    }
                }else{
                    Response.StatusCode = 401;
                        return new ObjectResult("Numero de cartão inexistente");
                }
            }catch(Exception e){
                Response.StatusCode = 401;
                return new ObjectResult(""); 
            }           
            
        }

        [Authorize]
        [HttpGet("saque/{caixaID}/{userID}/{valor}")]
        public IActionResult Saque( int caixaID, int userID, int valor){
            try{
                
                CaixaEletronico caixaEletronico = database.CaixaEletronicos.First(caixaEletronico=> caixaEletronico.Id == caixaID);
                var conta = database.Usuarios.First(u=> u.Id == userID);
                
                if (caixaEletronico != null){
                    if(valor%10 !=0){
                
                    return BadRequest("Valor Inválido.\nO valor deve ser múltiplo de 10");
                }

                if(valor > 1500){
                    return BadRequest("OPERAÇÃO NÃO PERMITIDA!\nO valor máximo para saques é de R$ 1.500,00");
                }
                
                if (conta.Saldo < valor){
                    return BadRequest("Saldo Insuficiente");
                }

                int totalDinheiro = caixaEletronico.Notas100*100+caixaEletronico.Notas50*50+caixaEletronico.Notas20*20+caixaEletronico.Notas10*10;
                
                if(valor > totalDinheiro){
                    return BadRequest("Valor muito alto \nQuantia insuficiente no Caixa ");
                }
                int n100=0, n50 =0, n20=0, n10=0;
                string print = "";
                int valorInicial = valor;
                do{
                    if(valor >= 100 && caixaEletronico.Notas100 > 0){
                        valor -= 100;
                        conta.Saldo -=100;
                        n100++;
                    }else if(valor >= 50 && caixaEletronico.Notas50 > 0){
                        valor -=50;
                        conta.Saldo -= 50;
                        n50++;
                    }else if(valor >= 20 && caixaEletronico.Notas20 > 0){
                        valor -= 20;
                        conta.Saldo -= 20;
                        n20++;
                    }else if(valor >= 10&& caixaEletronico.Notas10 > 0){
                        valor-=10;
                        conta.Saldo -= 10;
                        n10++;                     
                    }
                }while(valor > 0);

                caixaEletronico.Notas100 -= n100;
                caixaEletronico.Notas50 -= n50;
                caixaEletronico.Notas20 -= n20;
                caixaEletronico.Notas10 -= n10;
                if(n100 >0){
                    print += n100+" nota(s) de R$ 100,00 \n";
                }
                if(n50>0){
                    print += n50+" nota(s) de R$ 50,00 \n";
                }
                if(n20>0){
                    print += n20+" nota(s) de R$ 20,00 \n";
                }
                if(n10 >0){
                    print += n10+" nota(s) de R$ 10,00 \n";
                }
                
                int totalDeNotas = n100+n50+n20+n10;
                database.SaveChanges();
                
                return Ok("Saque efetuado no valor de: R$ "+valorInicial+",00\n\n"+"Voce receberá: "+totalDeNotas+" notas\n"+print);

                } 

            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
            return Ok("Saque efetuado");
        }

        [Authorize]
        [HttpGet("extrato")]
        public IActionResult Extrato([FromBody]LoginDTO extrato){
            try{
                Usuario usuario = database.Usuarios.First(u=> u.NumeroCartao.Equals(extrato.NumeroCartao));
                
                if(usuario != null){
                    return Ok(usuario.Extrato());
                }else{
                    Response.StatusCode = 401;
                    return new ObjectResult("Usuario não encontrado");
                }

            }catch(Exception e){
                Response.StatusCode = 401;
                return new ObjectResult("Usuario não encontrado");
            }
            

        }
        
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletarUsuario(int id){
            try{
                Usuario user = database.Usuarios.First(u=>u.Id == id);
                database.Remove(user);
                database.SaveChanges();
                return Ok("Usuario Excluido com Sucesso!");
            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("Usuario não encontrado");
            }
        }

        [Authorize(Roles="Admin")]
        [HttpPatch]
        public IActionResult EditarUsuario([FromBody]Usuario usuario){
            if(usuario.Id > 0){
                 try{
                    var usuarioValido = database.Usuarios.First(u=> u.Id == usuario.Id );
                    
                    if(usuarioValido != null){
                        usuarioValido.Nome = usuario.Nome != null ? usuario.Nome : usuarioValido.Nome;
                        usuarioValido.NumeroCartao = usuario.NumeroCartao != null ? usuario.NumeroCartao : usuarioValido.NumeroCartao;
                        usuarioValido.Senha = usuario.Senha != null ? usuario.Senha : usuarioValido.Senha;
                        usuarioValido.Saldo = usuario.Saldo != 0 ? usuario.Saldo : usuarioValido.Saldo;
                        
                        database.SaveChanges();
                        return Ok("Atualização de Usuario feita com sucesso");
                    }                    
                    Response.StatusCode = 401;
                    return new ObjectResult("Usuario não encontrado");

                }catch (Exception e){
                    Response.StatusCode = 400;
                    return new ObjectResult("Usuario não encontrado");
                }    
            }else {
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O ID inválido"});
            }
        }

        
    }
}