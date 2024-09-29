using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Servicos
{
    public interface IAlunoService
    {
        //interface para implementação dos metodos no CRUD

        //Metodos para Aluno
        Task<IEnumerable<Aluno>> GetAluno();
        Task<Aluno> GetAluno(Guid id);
        Task<IEnumerable<Aluno>> GetAlunoByNome(string nome);
        Task<IEnumerable<Aluno>> GetAlunoByid( Guid id);
        Task<Aluno> GetAlunoById(Guid id);
        Task CreateAluno(Aluno aluno);
        Task UpdateAluno(Aluno aluno);
        Task DeleteAluno(Aluno aluno);
    }
}
