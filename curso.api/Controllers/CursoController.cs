using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using curso.api.Business.Entities;
using curso.api.Business.Repositories;
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
    private readonly ICursoRepository _cursoRepository;

    public CursoController(ICursoRepository cursoRepository)
    {
      _cursoRepository = cursoRepository;
    }

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
      Curso curso = new Curso();
      curso.Nome = cursoViewModelInput.Nome;
      curso.Descricao = cursoViewModelInput.Descricao;

      var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
      curso.CodigoUsuario = codigoUsuario;

      _cursoRepository.Adicionar(curso);
      _cursoRepository.Commit();

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

      var codigoUsuario = int.Parse(User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

      var cursos = _cursoRepository.ObterPorUsuario(codigoUsuario)
        .Select(s => new CursoViewModelOutput()
        {
          Nome = s.Nome,
          Descricao = s.Descricao,
          Login = s.Usuario.Login
        });

      return Ok(cursos);
    }
  }
}