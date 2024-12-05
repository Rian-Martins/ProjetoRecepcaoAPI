using api_db.Identity;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Servicos.Interfaces;

namespace ProjetoRecepcao.Servicos
{
  public class AlunoService : IAlunoService 
    {
        private readonly AppDbContext _context;

        public AlunoService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Aluno> GetAluno(Guid id)
        {
            var aluno = await _context.Alunos.FindAsync(id);
            return aluno;
        }

        public async Task<IEnumerable<Aluno>> GetAluno()
        {
            try
            {
                return await _context.Alunos.ToListAsync();
            }
            catch (Exception ex) 
            {
                throw new Exception($"Não foi possivel Listar os Alunos", ex);
            }
            
        }


        public async Task<IEnumerable<Aluno>> GetAlunoByid(Guid alunoId)
        {
            IEnumerable<Aluno> alunos = new List<Aluno>();

            if (alunoId != Guid.Empty)
            {
                alunos = await _context.Alunos.Where(n => n.AlunoId == alunoId).ToListAsync();
            }

            return alunos;
        }

        public async Task<Aluno> GetAlunoById(Guid alunoId)
        {
            return await _context.Alunos.FindAsync(alunoId);
        }
                

        public async Task<IEnumerable<Aluno>> GetAlunoByNome(string nome)
        {
            IEnumerable<Aluno> alunos = new List<Aluno>();

            if (!string.IsNullOrEmpty(nome))
            {
                alunos = await _context.Alunos.Where(n => n.Nome.Contains(nome)).ToListAsync();
            }

            return alunos;
        }


    public async Task CreateAluno(Aluno aluno)
    {
      using (var transaction = await _context.Database.BeginTransactionAsync())
      {
        aluno.AlunoId = Guid.Empty;

        try
        {
          if (aluno == null)
            throw new ArgumentNullException(nameof(aluno), "O objeto aluno não pode ser nulo.");

       
          _context.Alunos.Add(aluno);




          // Cria uma nova entrada na PlanilhaReposicao com os dados do aluno
          var planilhaReposicao = new PlanilhaReposicao
          {

            AlunoId = aluno.AlunoId,
            Nome = aluno.Nome,
            Horario = aluno.Horario,
            Data = (string)aluno.Data,
            Professor = aluno.Professor,
            DiaSemana = aluno.DiaSemana
          };

          
          var alunoReposicao = new alunosreposicao
          {
            AlunoId = aluno.AlunoId,
            Nome = aluno.Nome,
            Horario = aluno.Horario,
            DiaSemana = aluno.DiaSemana,
            Reagendamento = false
            
          };

          var criacaoPlanAlunos = new criacaoplanalunos
          {
            AlunoId = aluno.AlunoId,
            Nome = aluno.Nome,
            Horario = aluno.Horario
          };

          // Adiciona a planilha de reposição ao contexto
          _context.PlanilhaReposicaos.AddAsync(planilhaReposicao);
          _context.alunosreposicaos.AddAsync(alunoReposicao);
          _context.criacaoplanalunos.AddAsync(criacaoPlanAlunos);


          try
          {
            await _context.SaveChangesAsync();
          }
          catch (DbUpdateException ex)
          {
            // Exibe a mensagem da exceção interna
            Console.WriteLine(ex.InnerException?.Message);
            throw;
          }

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


    public async Task UpdateAluno(Aluno aluno)
        {
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O campo aluno não pode ser nulo");
            }

            // Verificar se o aluno existe no banco de dados
            var alunoExistente = await _context.Alunos
                .FirstOrDefaultAsync(a => a.AlunoId == aluno.AlunoId);

            if (alunoExistente == null)
            {
                Console.WriteLine($"Aluno com ID: {aluno.AlunoId} não encontrado");
                return;
            }

            // Atualizar os valores principais do Aluno
            _context.Entry(alunoExistente).CurrentValues.SetValues(aluno);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine("Erro de concorrência ao atualizar o aluno: " + ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao atualizar o aluno: " + ex.Message);
                throw;
            }
        }



        public async Task DeleteAluno(Aluno aluno)
        {
            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Aluno>> GetAlunoByData(string data, string horario)
        {
            return await _context.Alunos
                .Where(p => p.Data == data && p.Horario == horario)
                .ToListAsync();
        }

        //não está sendo utilizado
        public async Task<Aluno> GetAlunoBydata(Guid alunoId, string data)
        {
            return await _context.Alunos
                .Where(n => n.AlunoId == alunoId && n.Data == data) // Filtra por ID e data
                .OrderBy(n => n.Data) // Ordena pela data (pode não ser necessário se você espera um único aluno)
                .FirstOrDefaultAsync(); // Retorna o primeiro aluno ou null se não encontrado
        }
    }
}
