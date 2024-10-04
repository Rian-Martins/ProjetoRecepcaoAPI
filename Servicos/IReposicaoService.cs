using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Servicos
{
    public interface IReposicaoService
    {
        Task<IEnumerable<PlanilhaReposicao>> GetReposicao();
        Task<PlanilhaReposicao> GetReposicao(Guid id);
        Task<IEnumerable<PlanilhaReposicao>> GetReposicaoByid(Guid id);
        Task<PlanilhaReposicao> GetReposicaoById(Guid id);
        Task<IEnumerable<PlanilhaReposicao>> GetAlunoByDataHorario(DateOnly data, string horario);
        //Task<PlanilhaReposicao> GetAlunoByDataHorario(Guid alunoId, DateOnly data);
        Task CreatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
        Task UpdatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
        Task DeletePlanilhaReposicao(PlanilhaReposicao planilhaReposicao);
    }
}
