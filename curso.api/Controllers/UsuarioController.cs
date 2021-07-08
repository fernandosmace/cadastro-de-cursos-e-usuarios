using curso.api.Models;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;
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
    public IActionResult Logar(LoginViewModelInput loginViewModelInput)
    {
      return Ok(loginViewModelInput);
    }

    [HttpPost]
    [Route("registrar")]
    public IActionResult Registrar(RegistroViewModelInput loginViewModelInput)
    {
      return Created("", loginViewModelInput);
    }
  }
}