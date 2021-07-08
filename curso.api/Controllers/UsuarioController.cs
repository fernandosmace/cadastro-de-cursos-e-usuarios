using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc;

namespace curso.api.Controllers
{
  [Route("api/v1/usuario")]
  [ApiController]
  public class UsuarioController : ControllerBase
  {
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