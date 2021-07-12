using System.ComponentModel.DataAnnotations;

namespace curso.api.Controllers
{
  public class CursoViewModelInput
  {
    [Required(ErrorMessage = "O nome do curso é obrigatório!")]
    public string Nome { get; set; }

    [Required(ErrorMessage = "A descriçaõ do curso é obrigatória!")]
    public string Descricao { get; set; }
  }
}