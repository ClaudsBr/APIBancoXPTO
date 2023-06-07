using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using XPTO.Data;
using XPTO.DTO;
using XPTO.Models;

namespace XPTO.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CaixasController: ControllerBase
    {
        private readonly ApplicationDbContext database;
        public CaixasController(ApplicationDbContext database){
            this.database = database;
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult ListarCaixasEletronicos(){
            var caixas = database.CaixaEletronicos.ToList();
            return Ok(caixas);
        }

               
        [Authorize(Roles="Admin")]
        [HttpGet("{id}")]
        public IActionResult ContagemDasNotasPelaIDdoCaixaEletronico(int id){
            try{
            CaixaEletronico caixaEletronico = database.CaixaEletronicos.First(c=> c.Id == id); 
            string print ="";
            if(caixaEletronico.Notas100 > 0){ 
                print +=caixaEletronico.Notas100+" nota(s) de R$ 100,00\n";
            }
            if(caixaEletronico.Notas50 > 0){ 
                print +=caixaEletronico.Notas50+" nota(s) de R$ 50,00\n";
            }
            if(caixaEletronico.Notas20 > 0){ 
                print +=caixaEletronico.Notas20+" nota(s) de R$ 20,00\n";
            }
            if(caixaEletronico.Notas10 > 0){ 
                print +=caixaEletronico.Notas10+" nota(s) de R$ 10,00\n";
            }
            
            return Ok("Notas Disponíveis neste Terminal:\n"+print);

            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("");
            }
            
        }

        [Authorize(Roles="Admin")]
        [HttpPost]
        public IActionResult CriarCaixa(CaixaEletronicoDTO caixaDTO){
            CaixaEletronico caixa = new CaixaEletronico();
            caixa.Notas10 = caixaDTO.Notas10;
            caixa.Notas20 = caixaDTO.Notas20;
            caixa.Notas50 = caixaDTO.Notas50;
            caixa.Notas100 = caixaDTO.Notas100;
            database.Add(caixa);
            database.SaveChanges();
            Response.StatusCode = 201;
            return new ObjectResult("Caixa Eletronico Criado com sucesso!");
        }

       
        [Authorize(Roles="Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeletandoCaixa(int id){
            try{
                CaixaEletronico caixa = database.CaixaEletronicos.First(c=>c.Id == id);
                database.Remove(caixa);
                database.SaveChanges();
                return Ok("Caixa Eletrônico Excluido com Sucesso");
            }catch(Exception e){
                Response.StatusCode = 404;
                return new ObjectResult("Caixa Eletrônico Não Encontrado");
            }
        }

        [Authorize(Roles="Admin")]
        [HttpPatch]
        public IActionResult EditarCaixa([FromBody] CaixaEletronico caixa){
            
            if(caixa.Id > 0){

                try{
                    var caixaValido = database.CaixaEletronicos.First(ct=> ct.Id == caixa.Id );
                    
                    if(caixaValido != null){

                        caixaValido.Notas10 = caixa.Notas10 != 0 ? caixa.Notas10 : caixaValido.Notas10;
                        caixaValido.Notas20 = caixa.Notas20 != 0 ? caixa.Notas20 : caixaValido.Notas20;
                        caixaValido.Notas50 = caixa.Notas50 != 0 ? caixa.Notas50 : caixaValido.Notas50;
                        caixaValido.Notas100 = caixa.Notas100 != 0 ? caixa.Notas100 : caixaValido.Notas100;
                        
                        database.SaveChanges();
                        return Ok();

                    }else{
                        Response.StatusCode = 400;
                        return new ObjectResult("Caixa não encontrado");
                    }
                }catch (Exception e){
                    Response.StatusCode = 400;
                    return new ObjectResult("Caixa não encontrado");
                }    

            }else{
                Response.StatusCode = 400;
                return new ObjectResult(new {msg = "O ID deste Caixa é inválido"});
            }
        }
    }
}