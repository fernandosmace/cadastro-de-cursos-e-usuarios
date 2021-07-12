using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AutoBogus;
using curso.api.Models.Usuarios;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace curso.api.tests.Integrations.Controllers
{
  public class UsuarioControllerTests : IClassFixture<WebApplicationFactory<Startup>>, IAsyncLifetime
  {
    private readonly WebApplicationFactory<Startup> _factory;
    private readonly HttpClient _httpClient;
    private readonly ITestOutputHelper _output;
    protected RegistroViewModelInput RegistroViewModelInput;

    public UsuarioControllerTests(WebApplicationFactory<Startup> factory, ITestOutputHelper output)
    {
      _factory = factory;
      _httpClient = _factory.CreateClient();
      _output = output;
    }

    [Fact]
    public async Task Registrar_InformandoUsuarioESenha_DeveRetornarSucesso()
    {
      // Arrange
      RegistroViewModelInput = new AutoFaker<RegistroViewModelInput>()
                                        .RuleFor(p => p.Email, faker => faker.Person.Email);

      StringContent content = new StringContent(JsonConvert.SerializeObject(RegistroViewModelInput), Encoding.UTF8,
        "application/json");

      // Act
      var httpClientRequest = await _httpClient.PostAsync("/api/v1/usuario/registrar", content);

      // Assert
      Assert.Equal(System.Net.HttpStatusCode.Created, httpClientRequest.StatusCode);
    }

    [Fact]
    public async Task Logar_InformandoUsuarioESenhaExistentes_DeveRetornarSucesso()
    {
      // Arrange
      var loginViewModelInput = new LoginViewModelInput
      {
        Login = RegistroViewModelInput.Login,
        Senha = RegistroViewModelInput.Senha
      };

      StringContent content = new StringContent(JsonConvert.SerializeObject(loginViewModelInput), Encoding.UTF8,
        "application/json");

      // Act
      var httpClientRequest = await _httpClient.PostAsync("/api/v1/usuario/logar", content);

      var loginViewModelOutput = JsonConvert.DeserializeObject<LoginViewModelOutput>(await httpClientRequest.Content.ReadAsStringAsync());

      // Assert
      Assert.Equal(System.Net.HttpStatusCode.OK, httpClientRequest.StatusCode);
      Assert.NotNull(loginViewModelOutput.Token);
      Assert.Equal(loginViewModelInput.Login, loginViewModelOutput.Usuario.Login);
    }

    public async Task InitializeAsync()
    {
      await Registrar_InformandoUsuarioESenha_DeveRetornarSucesso();
    }

    public async Task DisposeAsync()
    {
      _httpClient.Dispose();
    }
  }
}