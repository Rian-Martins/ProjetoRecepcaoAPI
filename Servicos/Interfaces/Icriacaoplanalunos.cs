using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using api_db.Identity;

namespace ProjetoRecepcao.Servicos.Interfaces
{
  public interface ICriacaoPlanAlunosService
  {
    Task<IEnumerable<criacaoplanalunos>> GetAllPlanosAsync();
    Task<criacaoplanalunos> GetPlanoByIdAsync(Guid alunoId);
    Task<IEnumerable<criacaoplanalunos>> GetPlanosByNomeAsync(string nome);
    Task<IEnumerable<criacaoplanalunos>> GetPlanosByDataAsync(DateOnly dataNovaMarcacao);
    Task<IEnumerable<criacaoplanalunos>> GetPlanosByHorarioAsync(string horario);
    Task CreatePlanoAsync(criacaoplanalunos plano);
    Task UpdatePlanoAsync(criacaoplanalunos plano);
    Task DeletePlanoAsync(Guid alunoId);
  }
}
