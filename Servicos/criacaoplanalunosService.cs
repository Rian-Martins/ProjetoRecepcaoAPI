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
  public class CriacaoPlanAlunosService : ICriacaoPlanAlunosService
  {
    private readonly AppDbContext _context;

    public CriacaoPlanAlunosService(AppDbContext context)
    {
      _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public async Task<IEnumerable<criacaoplanalunos>> GetAllPlanosAsync()
    {
      return await _context.criacaoplanalunos.ToListAsync();
    }

    public async Task<criacaoplanalunos> GetPlanoByIdAsync(Guid alunoId)
    {
      return await _context.criacaoplanalunos.FindAsync(alunoId);
    }

    public async Task<IEnumerable<criacaoplanalunos>> GetPlanosByDataAsync(DateOnly dataNovaMarcacao)
    {
      return await _context.criacaoplanalunos
          .Where(p => p.DataNovaMarcacao == dataNovaMarcacao)
          .ToListAsync();
    }

    public async Task<IEnumerable<criacaoplanalunos>> GetPlanosByHorarioAsync(string horario)
    {
      if (string.IsNullOrWhiteSpace(horario))
        throw new ArgumentException("O horário não pode ser vazio ou nulo.", nameof(horario));

      return await _context.criacaoplanalunos
          .Where(p => p.Horario.Contains(horario))
          .ToListAsync();
    }

    public async Task<IEnumerable<criacaoplanalunos>> GetPlanosByNomeAsync(string nome)
    {
      if (string.IsNullOrWhiteSpace(nome))
        throw new ArgumentException("O nome não pode ser vazio ou nulo.", nameof(nome));

      return await _context.criacaoplanalunos
          .Where(p => p.Nome.Contains(nome))
          .ToListAsync();
    }

    public async Task CreatePlanoAsync(criacaoplanalunos plano)
    {
      if (plano == null)
        throw new ArgumentNullException(nameof(plano), "O objeto plano não pode ser nulo.");

      plano.AlunoId = Guid.NewGuid();
      _context.criacaoplanalunos.Add(plano);
      await _context.SaveChangesAsync();
    }

    public async Task UpdatePlanoAsync(criacaoplanalunos plano)
    {
      if (plano == null)
        throw new ArgumentNullException(nameof(plano), "O objeto plano não pode ser nulo.");

      var existingPlano = await _context.criacaoplanalunos.FindAsync(plano.AlunoId);
      if (existingPlano == null)
        throw new KeyNotFoundException($"Plano com ID {plano.AlunoId} não encontrado.");

      _context.Entry(existingPlano).CurrentValues.SetValues(plano);
      await _context.SaveChangesAsync();
    }

    public async Task DeletePlanoAsync(Guid alunoId)
    {
      var plano = await _context.criacaoplanalunos.FindAsync(alunoId);
      if (plano == null)
        throw new KeyNotFoundException($"Plano com ID {alunoId} não encontrado.");

      _context.criacaoplanalunos.Remove(plano);
      await _context.SaveChangesAsync();
    }
  }
}
