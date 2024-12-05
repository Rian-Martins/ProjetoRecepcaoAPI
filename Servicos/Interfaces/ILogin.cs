using ProjetoRecepcao.Identidade.Gestores;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoRecepcao.Servicos.Interfaces
{
  public interface ILoginService
  {
    Task<IEnumerable<Login>> GetAllLoginsAsync();
    Task<Login> GetLoginByIdAsync(Guid id);
    Task<IEnumerable<Login>> GetLoginsByNomeAsync(string nome);
    Task<Login> AuthenticateAsync(string email, string senha);
    Task<Login> AuthenticateByEmailAsync(string email, string senha);
    Task<Login> AuthenticateByNameAsync(string nome, string senha);
    Task CreateLoginAsync(Login login);
    Task UpdateLoginAsync(Login login);
    Task DeleteLoginAsync(Guid id);
  }
}
