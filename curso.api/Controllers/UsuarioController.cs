using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using curso.api.Filters;
using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.api.Controllers
{
  [Route("api/v1/usuario")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
    //   <sumary>
    //   Este serviço permite autenticar um usuário cadastrado e ativo.
    //   </sumary>
    //   <param name="loginViewModelInput">View Model do login</param>
    //   <returns>Retorna status ok, dados do usuário e o token, em caso de sucesso.</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
    [HttpPost]
    [Route("logar")]
    [ValidacaoModelStateCustomizado]
    public IActionResult Logar(LoginViewModelInput loginViewModelInput)
    {

      var usuarioViewModelOutput = new UsuarioViewModelOutput()
      {
        Codigo = 1,
        Login = "fernando.macedo",
        Email = "fernando@macedo.com.br"
      };

      var secret = Encoding.ASCII.GetBytes("QhvE%pMvOsP1NHylHV#I5N^$RrJE6sa#lKK7k4ciGEnm3K9SFV");
      var symmetricSecurityKey = new SymmetricSecurityKey(secret);
      var securityTokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new Claim[]
          {
                    new Claim(ClaimTypes.NameIdentifier, usuarioViewModelOutput.Codigo.ToString()),
                    new Claim(ClaimTypes.Name, usuarioViewModelOutput.Login.ToString()),
                    new Claim(ClaimTypes.Email, usuarioViewModelOutput.Email.ToString()),
          }),
        Expires = DateTime.UtcNow.AddDays(1),
        SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
      };
      var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
      var tokenGenerated = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
      var token = jwtSecurityTokenHandler.WriteToken(tokenGenerated);


      return Ok(new
      {
        Token = token,
        Usuario = usuarioViewModelOutput

      });
    }

    [HttpPost]
    [Route("registrar")]
    [ValidacaoModelStateCustomizado]
    public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
    {
      return Created("", loginViewModelInput);
    }
  }
}