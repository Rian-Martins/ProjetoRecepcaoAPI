using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Identidade.Gestores;
using ProjetoRecepcao.Servicos.Interfaces;

namespace ProjetoRecepcao.Servicos
{
  public class LoginService : ILoginService
  {
    private readonly AppDbContext _context;

    public LoginService(AppDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<Login> AuthenticateAsync(string email, string senha)
    {
      // Valide os parâmetros de entrada
      if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(senha))
      {
        return null;
      }

      // Busca o usuário no banco de dados pelo email
      var user = await _context.logins.FirstOrDefaultAsync(l => l.Email == email);
      if (user == null)
      {
        return null; // Retorna null se o usuário não existe
      }

      // Verifica se a senha está correta
      bool senhaValida = BCrypt.Net.BCrypt.Verify(senha, user.Senha);
      if (!senhaValida)
      {
        return null; // Retorna null se a senha está incorreta
      }

      // Retorna o usuário autenticado
      return user;
    }



    public async Task<Login> AuthenticateByEmailAsync(string email, string senha)
    {
      // Busca o usuário pelo email
      var user = await _context.logins.FirstOrDefaultAsync(l => l.Email == email);

      if (user == null)
      {
        return null; // Retorna null se o usuário não for encontrado
      }

      // Verifica a senha usando BCrypt
      if (!BCrypt.Net.BCrypt.Verify(senha, user.Senha))
      {
        return null; // Retorna null se a senha estiver incorreta
      }

      return user; // Retorna o objeto Login, incluindo o Nome
    }





    public async Task<Login> AuthenticateByNameAsync(string nome, string senha)
    {
      return await _context.logins
          .FirstOrDefaultAsync(l => l.Nome == nome && l.Senha == senha);
    }

    public async Task<IEnumerable<Login>> GetAllLoginsAsync()
    {
      return await _context.logins.ToListAsync();
    }

    public async Task<Login> GetLoginByIdAsync(Guid id)
    {
      return await _context.logins.FindAsync(id);
    }

    public async Task<IEnumerable<Login>> GetLoginsByNomeAsync(string nome)
    {
      if (string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(nome));

      return await _context.logins
          .Where(l => l.Nome.Contains(nome))
          .ToListAsync();
    }

    public async Task CreateLoginAsync(Login login)
    {
      if (login == null)
        throw new ArgumentNullException(nameof(login), "O objeto login não pode ser nulo.");

      login.Id = Guid.NewGuid();

      
      _context.logins.Add(login);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateLoginAsync(Login login)
    {
      if (login == null)
        throw new ArgumentNullException(nameof(login), "O objeto login não pode ser nulo.");

      var existingLogin = await _context.logins.FindAsync(login.Id);
      if (existingLogin == null)
        throw new KeyNotFoundException($"Login com ID {login.Id} não encontrado.");

      _context.Entry(existingLogin).CurrentValues.SetValues(login);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteLoginAsync(Guid id)
    {
      var login = await _context.logins.FindAsync(id);
      if (login == null)
        throw new KeyNotFoundException($"Login com ID {id} não encontrado.");

      _context.logins.Remove(login);
      await _context.SaveChangesAsync();
    }
  }
}
