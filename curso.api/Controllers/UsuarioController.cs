using System.Threading.Tasks;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;
using curso.api.Configurations;
using curso.api.Filters;
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
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IAuthenticationService _authenticationService;

    public UsuarioController(IUsuarioRepository usuarioRepository, IAuthenticationService authenticationService)
    {
      _usuarioRepository = usuarioRepository;
      _authenticationService = authenticationService;
    }

    ///   <summary>
    ///   Este serviço permite autenticar um usuário cadastrado e ativo.
    ///   </summary>
    ///   <param name="loginViewModelInput">View Model do login</param>
    ///   <returns>Retorna status ok, dados do usuário e o token, em caso de sucesso.</returns>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao autenticar", Type = typeof(LoginViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
    [HttpPost]
    [Route("logar")]
    [ValidacaoModelStateCustomizado]
    public async Task<IActionResult> Logar(LoginViewModelInput loginViewModelInput)
    {
      var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

      if (usuario == null)
      {
        return BadRequest("Houve um erro ao tentar realizar o login.");
      }

      // if (usuario.Senha != loginViewModel.Senha.GerarSenhaCriptografada())
      // {
      //   return BadRequest("Houve um erro ao tentar realizar o login.");
      // }

      var usuarioViewModelOutput = new UsuarioViewModelOutput()
      {
        Codigo = usuario.Codigo,
        Login = loginViewModelInput.Login,
        Email = usuario.Email
      };

      var token = _authenticationService.GerarToken(usuarioViewModelOutput);

      return Ok(new LoginViewModelOutput
      {
        Token = token,
        Usuario = usuarioViewModelOutput
      });
    }

    ///   <summary>
    ///   Este serviço permite cadastrar um novo usuário.
    ///   </summary>
    ///   <param name="loginViewModelInput">View Model de cadastro</param>
    [SwaggerResponse(statusCode: 200, description: "Sucesso ao cadastrar novo usuário", Type = typeof(LoginViewModelInput))]
    [SwaggerResponse(statusCode: 400, description: "Campos obrigatórios", Type = typeof(ValidaCampoViewModelOutput))]
    [SwaggerResponse(statusCode: 500, description: "Erro interno", Type = typeof(ErroGenericoViewModel))]
    [HttpPost]
    [Route("registrar")]
    [ValidacaoModelStateCustomizado]
    public async Task<IActionResult> Registrar(RegistroViewModelInput loginViewModelInput)
    {
      var usuario = await _usuarioRepository.ObterUsuarioAsync(loginViewModelInput.Login);

      if (usuario != null)
      {
        return BadRequest("Usuário já cadastrado.");
      }

      usuario = new Usuario();
      usuario.Login = loginViewModelInput.Login;
      usuario.Senha = loginViewModelInput.Senha;
      usuario.Email = loginViewModelInput.Email;

      _usuarioRepository.Adicionar(usuario);
      _usuarioRepository.Commit();

      return Created("", loginViewModelInput);
    }
  }
}