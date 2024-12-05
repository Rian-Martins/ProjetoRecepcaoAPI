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
  public class CadastroService : ICadastroService
  {
    private readonly AppDbContext _context;

    public CadastroService(AppDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<Cadastro>> GetAllCadastrosAsync()
    {
      return await _context.cadastros.ToListAsync();
    }

    public async Task<Cadastro> GetCadastroByIdAsync(Guid id)
    {
      return await _context.cadastros.FindAsync(id);
    }

    public async Task<IEnumerable<Cadastro>> GetCadastrosByNomeAsync(string nome)
    {
      if (string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(nome));

      return await _context.cadastros
          .Where(c => c.Nome.Contains(nome))
          .ToListAsync();
    }


    //metodo que salva tanto na tabela de cadastro, quanto na tela de login
    public async Task CreateCadastroAsync(Cadastro cadastro)
    {
      using (var transaction = await _context.Database.BeginTransactionAsync())
      {

        cadastro.Id = Guid.NewGuid();
        
        try
        {
          var existenteCadastro = await _context.cadastros.AnyAsync(c => c.Id == cadastro.Id);
          if (existenteCadastro)
          {
             Console.WriteLine("Id de Cadastro já existente");
          }
          cadastro.Senha = BCrypt.Net.BCrypt.HashPassword(cadastro.Senha);
          await _context.cadastros.AddAsync(cadastro);

          // Cria o objeto de login correspondente
          var login = new Login
          {
            Id = cadastro.Id,
            Nome = cadastro.Nome,
            Email = cadastro.Email,
            Senha = cadastro.Senha,
            Descricao = cadastro.Descricao,
            Funcao = cadastro.Funcao
            
            // Outras propriedades, se necessário
          };

          // Adiciona o login
          await _context.logins.AddAsync(login);

          try
          {
            // Salva as alterações
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateException ex)
          {
            // Exibe a mensagem da exceção interna
            Console.WriteLine(ex.InnerException?.Message);
            throw;
          }


          // Confirma a transação
          await transaction.CommitAsync();
        }
        catch (Exception)
        {
          //Reverte a transação em caso de erro
         await transaction.RollbackAsync();
          throw;
        }
      }
    }
  


    public async Task UpdateCadastroAsync(Cadastro cadastro)
    {
      if (cadastro == null)
        throw new ArgumentNullException(nameof(cadastro), "O objeto cadastro não pode ser nulo.");

      var existingCadastro = await _context.cadastros.FindAsync(cadastro.Id);
      if (existingCadastro == null)
        throw new KeyNotFoundException($"Cadastro com ID {cadastro.Id} não encontrado.");

      _context.Entry(existingCadastro).CurrentValues.SetValues(cadastro);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteCadastroAsync(Guid id)
    {
      var cadastro = await _context.cadastros.FindAsync(id);
      if (cadastro == null)
        throw new KeyNotFoundException($"Cadastro com ID {id} não encontrado.");

      _context.cadastros.Remove(cadastro);
      await _context.SaveChangesAsync();
    }
  }
}
