using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Servicos.Interfaces
{
  public interface IReposicaoService
  {
    Task<IEnumerable<PlanilhaReposicao>> GetReposicao();
    Task<PlanilhaReposicao> GetReposicao(Guid alunoId);
    Task<IEnumerable<PlanilhaReposicao>> GetReposicaoByid(Guid alunoId);
    Task<PlanilhaReposicao> GetReposicaoById(Guid alunoId);
    Task<IEnumerable<PlanilhaReposicao>> GetAlunoByDataHorario(string data, string horario);
    //Task<PlanilhaReposicao> GetAlunoByDataHorario(Guid alunoId, DateOnly data);

    Task<IList<PlanilhaReposicao>> CreatePlanilhasGrande(List<PlanilhaReposicao> planilhas);
    Task CreatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
    Task AddAlunosReposicao(List<PlanilhaReposicao> planilhaReposicaos);
    Task UpdatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
    Task DeletePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
    Task CreatePlanilhaReposicao(List<PlanilhaReposicao> planilhas);
  }
}
