using System.ComponentModel.DataAnnotations;

namespace ProjetoRecepcao.Identidade.Gestores
{
  public class Login
  {
    public Guid Id { get; set; }

    
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email é obrigatório.")]
    [EmailAddress(ErrorMessage = "O email fornecido não é válido.")]
    public string Email { get; set; }

    [Required(ErrorMessage = "A senha é obrigatória.")]
    public string Senha { get; set; }
    public string? Funcao { get; set; }

    public string? Descricao { get; set; }
    public Login() { }

    public Login( string nome, string email, string senha, string funcao, string descricao)
    {
      Id =Guid.NewGuid();
      Nome = nome;
      Email = email;
      Senha = senha;
      Funcao = funcao;
      Descricao = descricao;
    }
  }
}
