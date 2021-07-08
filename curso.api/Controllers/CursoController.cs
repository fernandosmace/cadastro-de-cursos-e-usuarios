using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using curso.api.Models;
using curso.api.Models.Cursos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace curso.api.Controllers
{
  [Route("api/v1/curso")]
  [ApiController]
  [Authorize]
  public class CursoController : ControllerBase
  {


    //   <sumary>
    //   Este serviço permite cadastrar curso para o usuário autenticado.
    //   </sumary>
    //   <returns>Retorna status 201 e dados do curso do usuário.</returns>
    [SwaggerResponse(statusCode: 201, description: "Curso cadastrado com sucesso!", Type = typeof(CursoViewModelInput))]
    [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidaCampoViewModelOutput))]
    [HttpPost]
    [Route("cadastro")]
    public async Task<IActionResult> Post(CursoViewModelInput cursoViewModelInput)
    {
      //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

      return Created("", cursoViewModelInput);
    }

    //   <sumary>
    //   Este serviço permite obter todos os cursos ativos do usuário.
    //   </sumary>
    //   <returns>Retorna status ok, dados do curso do usuário.</returns>
    [SwaggerResponse(statusCode: 201, description: "Curso obtidos com sucesso!", Type = typeof(CursoViewModelInput))]
    [SwaggerResponse(statusCode: 401, description: "Não autorizado", Type = typeof(ValidaCampoViewModelOutput))]
    [HttpGet]
    [Route("listarCursos")]
    public async Task<IActionResult> Get()
    {
      var cursos = new List<CursoViewModelOutput>();

      //var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

      cursos.Add(new CursoViewModelOutput()
      {
        Login = "",
        Descricao = "testeDescricao",
        Nome = "testeNome"
      });

      return Ok(cursos);
    }
  }
}