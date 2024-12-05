using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRecepcao.Identidade.Gestores
{
  public class Cadastro
  {
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Senha { get; set; }
    public string Funcao { get; set; }

    public string Descricao { get; set; }

    public Cadastro() { }

    public Cadastro(string nome,string email,string senha, string funcao, string descricao)
    {
      Id = Guid.NewGuid();
      Nome = nome;
      Email = email;
      Senha = senha;
      Funcao = funcao;
      Descricao = descricao;
    }
  }
}

