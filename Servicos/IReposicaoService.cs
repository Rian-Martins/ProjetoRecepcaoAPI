using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Servicos
{
    public interface IReposicaoService
    {
        Task<IEnumerable<PlanilhaReposicao>> GetReposicao();
        Task<PlanilhaReposicao> GetReposicao(Guid id);
        Task<IEnumerable<PlanilhaReposicao>> GetReposicaoByData(DateOnly data);
        Task<PlanilhaReposicao> GetReposicaoBydata(Guid alunoId, DateOnly data);
        Task CreatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
        Task UpdatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
        Task DeletePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
    }
}
