using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRecepcao.Identidade
{
  [Table("PlanilhaReposicao")]
  public class PlanilhaReposicao
  {

    [Key]
    public Guid AlunoId { get; set; }
    public string? Nome { get; set; }
    public string? Horario { get; set; }
    public string? Data { get; set; }
    public string? Professor { get; set; }
    public string? DiaSemana { get; set; }




    public PlanilhaReposicao() { }
    public PlanilhaReposicao(Guid alunoId, string? nome, string horario, string data, string? professor, string diaSemana)
    {
      AlunoId = alunoId;
      Nome = nome;
      Horario = horario;
      Data = data;
      Professor = professor;
      DiaSemana = diaSemana;
    }

    //criacao de varias planilhas
    public class AlunoHorario
    {
      public string? Nome { get; set; }
      public string? Horario { get; set; }
    }

    public class HorariosPorDia
    {
      public List<AlunoHorario> Segunda { get; set; }
      public List<AlunoHorario> Terca { get; set; }
      public List<AlunoHorario> Quarta { get; set; }
      public List<AlunoHorario> Quinta { get; set; }
      public List<AlunoHorario> Sexta { get; set; }
    }
  }
}
