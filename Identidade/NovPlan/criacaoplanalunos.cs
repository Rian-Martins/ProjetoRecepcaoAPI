using ProjetoRecepcao.Identidade;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_db.Identity
{
  public class criacaoplanalunos
  {
    public Guid Id { get; set; } 
    public Guid? AlunoId { get; set; }
    public string? Nome { get; set; }
    public DateOnly DataNovaMarcacao { get; set; }
    public string? Horario { get; set; }


    public criacaoplanalunos() { }
    public criacaoplanalunos(string nome, DateOnly dataNovaMarcacao, string horario)
    {
      Id = Guid.NewGuid();
      AlunoId = Guid.NewGuid();
      Nome = nome;
      DataNovaMarcacao = dataNovaMarcacao;
      Horario = horario;
    }
  }

}
