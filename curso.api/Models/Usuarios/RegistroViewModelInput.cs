using System.ComponentModel.DataAnnotations;

namespace curso.api.Models.Usuarios
{
  public class RegistroViewModelInput
  {
    [Required(ErrorMessage = "O Login é obrigatório!")]
    public string Login { get; set; }

    [Required(ErrorMessage = "O email é obrigatório!")]
    [EmailAddress(ErrorMessage = "E-mail inválido!")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória!")]
    public string Senha { get; set; }

  }
}