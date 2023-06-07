<h1  align="center">Desafio TDD Starters #3</h1>

  
<h2><b>Desafio:</b></h2>

 
• Objetivo: Aplicar os conceitos de testes aprendidos durante a semana de estudo.


<h2>Testes unitários</h2>
1. Desenvolva um programa que simule a entrega de notas quando um cliente efetuar um saque em um caixa eletrônico. Os requisitos básicos são os seguintes:<br>
• Entregar o menor número de notas;<br>
• É possível sacar o valor solicitado com as notas disponíveis;<br>
• Notas disponíveis de R$ 100,00; R$ 50,00; R$ 20,00 e R$ 10,00<br><br>
<b>Exemplos:</b><br> 
• <i>Valor do Saque: R$ 30,00</i> – Resultado Esperado: Entregar 1 nota de R$ 20,00 e 1 nota de R$ 10,00.<br>
• <i>Valor do Saque: R$ 80,00</i> – Resultado Esperado: Entregar 1 nota de R$ 50,00 ; 1 nota de R$ 20,00 e 1 nota de R$ 10,00.<br><br>
  <font  color=blue> O projeto de Teste esta contido na pasta <b>Testes</b> onde foram feitos 6 Testes para a <b>Classe Caixa</b> e 3 Testes para a <b>Classe Usuário</b> do Projeto API.<br><br>
  <b><h3>Testes Executados</b></h3><br>
  <b>Classe Caixa<br></b>
  •  TestandoOValorZero() - <font color=blue>Passou<br></font>
  •  TestandoOValor10() - <font  color=blue>Passou<br></font>
  •  TestandoOValor30() - <font  color=blue>Passou<br></font>
  •  TestandoOValor80() - <font  color=blue>Passou<br></font>
  •  TestandoOValor100() - <font  color=blue>Passou<br></font>
  •  TestandoOValor180() - <font  color=blue>Passou<br></font><br>
  <b>Classe Usuário<br></b>
  •  TestandoCartaoComNumeroInvalido() - <font color=blue>Passou<br></font>
  •  TestandoCartaoComNumeroValido() - <font  color=blue>Passou<br></font>
  •  TestandoExtrato() - <font  color=blue>Passou<br></font>
  
  
  
  
  <h2><b>Exceeds:</b></h2>

 <b>a. Contagem de notas no caixa</b><br>
•  Para cada nota, deverá ser possível acessar a quantidade de notas disponíveis sob um login administrador<br>
<font color=blue> A contagem de notas dos Caixas é feita apenas pelo perfil Admin atraves do verbo GET nos caminhos:</font><br>
Login de Admin (ja populado no BD): <br>
NumeroCartao = '0000000000000000'<br>
Senha = '@@@@@@'<br>
https://localhost:5001/api/usuarios <br>
https://localhost:5001/api/usuarios/{id} <br>
<br>
<b>b. Acesso ao caixa eletrônico via cartão </b> <br>
• login com 16 dígitos onde será aceito apenas as faixas de cartões pertencentes ao banco correspondente, representada pelos 4 primeiros dígitos do cartão<br>
<b>Exemplo:</b><br>
• o banco XPTO possui as faixas 1895 e 3567, então cartões com início diferente desses valores não serão aceitos<br>
<font color=blue>A validação do cartão é feita através do método CartaoValido() da classe Usuario</font><br>
<b>c. Ao acessar a própria conta, o usuário poderá acessar seu saldo atual e o mesmo deve ser atualizado com os saques realizados</b><br>
<font color=blue> Para ter acesso ao saldo o usuário deve estar registrado e logar (ambos atraves do verbo POST) fornecendo o numero do seu cartão e sua senha no corpo da requisição<br>
https://localhost:5001/api/usuarios/registro <br>
https://localhost:5001/api/usuarios/login <br>

Para acessar ao saldo da conta o usuario devera fornecer o numero do cartão e a sua senha no corpo da requisição (usa-se o verbo GET) <br>
https://localhost:5001/api/usuarios/extrato <br>

<font color=blue>Para fazer um saque o usuário deve enviar seu token de autorização e informar o ID do caixa onde efetuará o saque, além de fornecer sua ID e a respectiva quantia a ser sacada (usa-se o verbo GET):</font><br>
https://localhost:5001/api/usuarios/saque/{caixaID}/{userID}/{valor} <br>


_________________________________________________________________________
<h3 align="left">Contato:</h3>  
<p align="left">  
<a href="https://linkedin.com/in/claudioosbr" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="claudioosbr" height="30" width="40" /></a>  
</p>  
  
<h3 align="left">Linguagens e Ferramentas:</h3>  
<p align="left"> <a href="https://www.w3schools.com/cs/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/csharp/csharp-original.svg" alt="csharp" width="40" height="40"/> </a> <a href="https://dotnet.microsoft.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/dot-net/dot-net-original-wordmark.svg" alt="dotnet" width="40" height="40"/> </a> <a href="https://www.mysql.com/" target="_blank" rel="noreferrer"> <img src="https://raw.githubusercontent.com/devicons/devicon/master/icons/mysql/mysql-original-wordmark.svg" alt="mysql" width="40" height="40"/> </a> </p>