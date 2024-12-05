using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjetoRecepcao.Identidade
{
    [Table("PlanilhaReposicao")]
    public class PlanilhaReposicao
    {
        
        public Guid Id {  get; set; }        
        public Guid AlunoId { get; set; }
        public string? Nome { get; set; }
        public string? Horario { get; set; }
        public string? Data { get; set; }
        public string? Professor { get; set; }
        public string? DiaSemana { get; set; }

        


        public PlanilhaReposicao() { }
        public PlanilhaReposicao (Guid alunoId, string? nome, string horario, string data, string? professor, string diaSemana)
        {
            AlunoId = alunoId;
            Nome = nome;
            Horario = horario;
            Data = data;
            Professor = professor;
            DiaSemana = diaSemana;
        }
    }
}
