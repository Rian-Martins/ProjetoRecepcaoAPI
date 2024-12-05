using ProjetoRecepcao.Identidade;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_db.Identity
{
  public class alunosreposicao
  {
    public Guid Id { get; set; } // Nova propriedade para chave primária
    public Guid AlunoId { get; set; }
    public string? Nome { get; set; } 
    public string? Horario { get; set; }
    public string? DiaSemana { get; set; }
    public string? Observacoes { get; set; }
    public bool? Reagendamento { get; set; }

    public alunosreposicao()
    {
        
    }
    public alunosreposicao(string nome, string horario, string diaSemana, string observacoes, bool reagendamento)
    {
      AlunoId = Guid.NewGuid();
      Nome = nome;
      Horario = horario;
      DiaSemana = diaSemana;
      Observacoes = observacoes;
      Reagendamento = reagendamento;
    }

    

    public string ReagendarAluno()
    {
      return (bool)Reagendamento ? "Reagendamento de Aluno" : "Aluno não reagendado";
    }
  }

}
