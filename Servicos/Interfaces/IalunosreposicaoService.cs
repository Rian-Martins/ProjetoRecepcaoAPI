using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_db.Identity;

namespace ProjetoRecepcao.Servicos.Interfaces
{
  public interface IalunosreposicaoService
  {
    Task<IEnumerable<alunosreposicao>> GetAllAlunosReposicaoAsync();
    Task<alunosreposicao> GetAlunoReposicaoByIdAsync(Guid alunoId);
    Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByNomeAsync(string nome);
    Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByHorarioAsync(string horario);
    Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByDiaSemanaAsync(string diaSemana);
    Task CreateAlunoReposicaoAsync(alunosreposicao alunoReposicao);
    Task UpdateAlunoReposicaoAsync(alunosreposicao alunoReposicao);
    Task DeleteAlunoReposicaoAsync(Guid alunoId);
    Task<string> ReagendarAlunoAsync(Guid alunoId);
  }
}
