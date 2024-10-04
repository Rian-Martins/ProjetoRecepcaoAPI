using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Servicos
{
    public class ReposicaoService : IReposicaoService
    {
        private readonly AppDbContext _context;

        public ReposicaoService(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<PlanilhaReposicao>> GetReposicao()
        {
            try
            {
                return await _context.PlanilhaReposicaos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel Listar os Alunos", ex);
            }
        }

        public async Task<PlanilhaReposicao> GetReposicao(Guid id)
        {
            var alunoReposicao = await _context.PlanilhaReposicaos.FindAsync(id);
            return alunoReposicao;
        }

        public async Task<IEnumerable<PlanilhaReposicao>> GetReposicaoByid(Guid alunoId)
        {
            if (alunoId == Guid.Empty)
            {
                return new List<PlanilhaReposicao>();
            }

            // Busca as planilhas de reposição para o aluno especificado
            var alunosReposicao = await _context.PlanilhaReposicaos
                .Where(n => n.AlunoId == alunoId)
                .ToListAsync();

            return alunosReposicao; // Retorna a lista de planilhas de reposição
        }
        public async Task<PlanilhaReposicao> GetReposicaoById(Guid alunoId)
        {
            return await _context.PlanilhaReposicaos.FindAsync(alunoId);
        }

        public async Task CreatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {
            if (planilhaReposicao == null)
            {
                throw new ArgumentNullException(nameof(planilhaReposicao), "O campo planilha não pode ser nulo");
            }

            try
            {
                planilhaReposicao.AlunoId = Guid.NewGuid();



                 _context.PlanilhaReposicaos.Add(planilhaReposicao);

                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
            }                                    
            catch (Exception ex)
            {
                // Tratar a exceção de forma apropriada (log, rethrow, etc.)
                Console.WriteLine($"Erro ao criar planilha: {ex.Message}");
                throw;
            }
        }


        public async Task DeletePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {
            _context.PlanilhaReposicaos.Remove(planilhaReposicao);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<PlanilhaReposicao>> GetAlunoByDataHorario(DateOnly data, string horario)
        {
            return await _context.PlanilhaReposicaos
                .Where(p => p.Data == data && p.Horario == horario)
                .ToListAsync();
        }

        //não está sendo utilizado
        //public async Task<PlanilhaReposicao> GetAlunoByDataHorario(Guid alunoId, DateOnly data)
        //{
        //    return await _context.PlanilhaReposicaos
        //        .Where(n => n.AlunoId == alunoId && n.Data == data) // Filtra por ID e data
        //        .OrderBy(n => n.Data) // Ordena pela data (pode não ser necessário se você espera um único aluno)
        //        .FirstOrDefaultAsync(); // Retorna o primeiro aluno ou null se não encontrado
        //}

        

        public async Task UpdatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {
            if (planilhaReposicao == null)
            {
                throw new ArgumentNullException(nameof(planilhaReposicao), "O campo aluno não pode ser nulo");
            }

            // Verificar se o aluno existe no banco de dados
            var alunoExistente = await _context.PlanilhaReposicaos
                .FirstOrDefaultAsync(a => a.AlunoId == planilhaReposicao.AlunoId);

            if (alunoExistente == null)
            {
                Console.WriteLine($"Aluno com ID: {planilhaReposicao.AlunoId} não encontrado");
                return;
            }

            // Atualizar os valores principais do Aluno
            _context.Entry(alunoExistente).CurrentValues.SetValues(planilhaReposicao);

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

       
    }
}
