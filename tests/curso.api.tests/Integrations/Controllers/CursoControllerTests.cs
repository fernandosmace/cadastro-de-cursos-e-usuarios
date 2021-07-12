using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoBogus;
using curso.api.Controllers;
using curso.api.Models.Cursos;
using curso.api.tests.Configurations;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;

namespace curso.api.tests.Integrations.Controllers
{
    public class CursoControllerTests : UsuarioControllerTests
    {
        protected CursoViewModelInput CursoViewModelInput;

        public CursoControllerTests(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Cadastrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            // Arrange
            CursoViewModelInput = new AutoFaker<CursoViewModelInput>(AutoBogusConfiguration.LOCATE);

            StringContent content = new StringContent(JsonConvert.SerializeObject(CursoViewModelInput), Encoding.UTF8,
              "application/json");

            // Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientRequest = await _httpClient.PostAsync("/api/v1/curso/cadastro", content);

            // Assert          
            Assert.Equal(System.Net.HttpStatusCode.Created, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Cadastrar_InformandoDadosDeUmCursoValidoEUmUsuarioNaoAutenticado_DeveRetornarSucesso()
        {
            // Arrange
            CursoViewModelInput = new AutoFaker<CursoViewModelInput>(AutoBogusConfiguration.LOCATE);

            StringContent content = new StringContent(JsonConvert.SerializeObject(CursoViewModelInput), Encoding.UTF8,
              "application/json");

            // Act
            var httpClientRequest = await _httpClient.PostAsync("/api/v1/curso/cadastro", content);

            // Assert          
            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);
        }
        
        [Fact]
        public async Task Listar_InformandoUmUsuarioAutenticado_DeveRetornarSucesso()
        {
            // Arrange
            await Cadastrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso();

            // Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", LoginViewModelOutput.Token);
            var httpClientRequest = await _httpClient.GetAsync("/api/v1/curso/listarCursos");

            // Assert
            var cursos = JsonConvert.DeserializeObject<IList<CursoViewModelOutput>>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.NotEmpty(cursos);
            Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);
        }

        [Fact]
        public async Task Listar_InformandoUmUsuarioNaoAutenticado_DeveRetornarSucesso()
        {
            // Arrange
            await Cadastrar_InformandoDadosDeUmCursoValidoEUmUsuarioAutenticado_DeveRetornarSucesso();

            // Act
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            var httpClientRequest = await _httpClient.GetAsync("/api/v1/curso/listarCursos");

            // Assert
            var cursos = JsonConvert.DeserializeObject<IList<CursoViewModelOutput>>(await httpClientRequest.Content.ReadAsStringAsync());

            Assert.Equal(System.Net.HttpStatusCode.Unauthorized, httpClientRequest.StatusCode);
        }
    }
}