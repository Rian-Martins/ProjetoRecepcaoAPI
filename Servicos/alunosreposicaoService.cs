using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_db.Identity;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Servicos.Interfaces;

namespace ProjetoRecepcao.Servicos
{
  public class alunosreposicaoService : IalunosreposicaoService
  {

    private readonly AppDbContext _context;    

    public alunosreposicaoService(AppDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<alunosreposicao>> GetAllAlunosReposicaoAsync()
    {
      return await _context.alunosreposicaos.ToListAsync();
    }

    public async Task<alunosreposicao> GetAlunoReposicaoByIdAsync(Guid alunoId)
    {
      return await _context.alunosreposicaos.FindAsync(alunoId);
    }

    public async Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByNomeAsync(string nome)
    {
      if (string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(nome));

      return await _context.alunosreposicaos
          .Where(ar => ar.Nome.Contains(nome))
          .ToListAsync();
    }

    public async Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByHorarioAsync(string horario)
    {
      if (string.IsNullOrWhiteSpace(horario))
        throw new ArgumentException("O horário não pode ser vazio ou nulo.", nameof(horario));

      return await _context.alunosreposicaos
          .Where(ar => ar.Horario == horario)
          .ToListAsync();
    }

    public async Task<IEnumerable<alunosreposicao>> GetAlunosReposicaoByDiaSemanaAsync(string diaSemana)
    {
      return await _context.alunosreposicaos
          .Where(ar => ar.DiaSemana == diaSemana)
          .ToListAsync();
    }

    public async Task CreateAlunoReposicaoAsync(alunosreposicao alunoReposicao)
    {
      if (alunoReposicao == null)
        throw new ArgumentNullException(nameof(alunoReposicao), "O objeto alunoReposicao não pode ser nulo.");

      alunoReposicao.Id = Guid.NewGuid();
      _context.alunosreposicaos.Add(alunoReposicao);
      await _context.SaveChangesAsync();
    }

    public async Task UpdateAlunoReposicaoAsync(alunosreposicao alunoReposicao)
    {
      if (alunoReposicao == null)
        throw new ArgumentNullException(nameof(alunoReposicao), "O objeto alunoReposicao não pode ser nulo.");

      var existingAlunoReposicao = await _context.alunosreposicaos.FindAsync(alunoReposicao.Id);
      if (existingAlunoReposicao == null)
        throw new KeyNotFoundException($"AlunoReposicao com ID {alunoReposicao.Id} não encontrado.");

      _context.Entry(existingAlunoReposicao).CurrentValues.SetValues(alunoReposicao);
      await _context.SaveChangesAsync();
    }

    public async Task DeleteAlunoReposicaoAsync(Guid alunoId)
    {
      var alunoReposicao = await _context.alunosreposicaos.FindAsync(alunoId);
      if (alunoReposicao == null)
        throw new KeyNotFoundException($"AlunoReposicao com ID {alunoId} não encontrado.");

      _context.alunosreposicaos.Remove(alunoReposicao);
      await _context.SaveChangesAsync();
    }

    public async Task<string> ReagendarAlunoAsync(Guid alunoId)
    {
      var alunoReposicao = await _context.alunosreposicaos.FindAsync(alunoId);
      if (alunoReposicao == null)
        throw new KeyNotFoundException($"AlunoReposicao com ID {alunoId} não encontrado.");

      if ((bool)alunoReposicao.Reagendamento)
        return "Reagendamento de Aluno";

      return "Aluno não reagendado";
    }
  }
}
