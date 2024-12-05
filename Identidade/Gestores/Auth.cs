using System.ComponentModel.DataAnnotations;

namespace ProjetoRecepcao.Identidade.Gestores
{
  public class Auth
  {
    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; }

    public Auth()
    {
        
    }

    public Auth(string email, string senha)
    {
        Email = email;
        Senha = senha;
    }
  }
}
