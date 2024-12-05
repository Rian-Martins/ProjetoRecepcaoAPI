using ProjetoRecepcao.Identidade.Gestores;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoRecepcao.Servicos.Interfaces
{
  public interface ICadastroService
  {
    Task<IEnumerable<Cadastro>> GetAllCadastrosAsync();
    Task<Cadastro> GetCadastroByIdAsync(Guid id);
    Task<IEnumerable<Cadastro>> GetCadastrosByNomeAsync(string nome);
    Task CreateCadastroAsync(Cadastro cadastro);
    Task UpdateCadastroAsync(Cadastro cadastro);
    Task DeleteCadastroAsync(Guid id);
  }
}
