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

        


        //metodo para listar todos os dados
        public async Task<IEnumerable<PlanilhaReposicao>> GetReposicao()
        {
            try
            {
                return await _context.PlanilhaReposicaos.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possivel Listar as Datas", ex);
            }
        }
        //metodo para escolher um em especifico
        public async Task<PlanilhaReposicao> GetReposicao(Guid id)
        {
            var reposicaoDia = await _context.PlanilhaReposicaos.FindAsync(id);
            return reposicaoDia;
        }


        //metodo para listar os dados com as datas
        public async Task<IEnumerable<PlanilhaReposicao>> GetReposicaoByData(DateOnly data)
        {
            return await _context.PlanilhaReposicaos
                .Where(n => n.Data == data)
                .OrderBy(n => n.Data) // Ordena pela data
                .ToListAsync();
        }

        //metodo para listar somente um dado com a data em especifico
        public async Task<PlanilhaReposicao> GetReposicaoBydata(Guid alunoId, DateOnly data)
        {
            return await _context.PlanilhaReposicaos
                .Where(n => n.AlunoId == alunoId && n.Data == data) // Filtra por ID e data
                .OrderBy(n => n.Data) // Ordena pela data (pode não ser necessário se você espera um único aluno)
                .FirstOrDefaultAsync(); // Retorna o primeiro aluno ou null se não encontrado
        }


        public async Task CreatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {

            if (planilhaReposicao == null)
            {
                throw new ArgumentNullException(nameof(planilhaReposicao), "O campo planilhaReposicao não pode ser nulo");
            }

            try
            {
                // Gerar um novo ID para o aluno
                planilhaReposicao.AlunoId = Guid.NewGuid();


                _context.PlanilhaReposicaos.Add(planilhaReposicao);

               


                // Salvar as alterações no banco de dados
                await _context.SaveChangesAsync();
                //}

            }
            catch (Exception ex)
            {
                // Tratar a exceção de forma apropriada (log, rethrow, etc.)
                Console.WriteLine($"Erro ao criar aluno: {ex.Message}");
                throw;
            }
        }

       
        public Task UpdatePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {
            throw new NotImplementedException();
        }
        public Task DeletePlanilhaReposicao(PlanilhaReposicao planilhaReposicao)
        {
            throw new NotImplementedException();
        }
    }
}
