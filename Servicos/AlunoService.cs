using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Contexto;
using ProjetoRecepcao.Identidade;

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
            if (aluno == null)
            {
                throw new ArgumentNullException(nameof(aluno), "O campo aluno não pode ser nulo");
            }

            try
            {
                // Gerar um novo ID para o aluno
                aluno.AlunoId = Guid.NewGuid();


                _context.Alunos.Add(aluno);

                //abaixo e somente para gerar uma relação entre tabelas/identidades
                //if(aluno.AlunoHorarios != null)
                //{
                //    aluno.AlunoHorarios = new List<AlunoHorario>();
                //}
                //// Sincronizar os horários do Aluno com os AlunoHorarios
                //foreach (var alunoHorario in aluno.AlunoHorarios)
                //{
                //    alunoHorario.Id = Guid.NewGuid(); // Garante que o Id de cada AlunoHorario seja único                    
                //    alunoHorario.Horario = aluno.Horario; // Sincroniza o Horario de Aluno para AlunoHorario
                //    alunoHorario.Aluno = aluno;

                //    // Adicionar o novo aluno ao contexto
                    

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

        public async Task<IEnumerable<Aluno>> GetAlunoByData(DateOnly data)
        {
            return await _context.Alunos
                .Where(p => p.Data == data)
                .ToListAsync();
        }

        //não está sendo utilizado
        public async Task<Aluno> GetAlunoBydata(Guid alunoId, DateOnly data)
        {
            return await _context.Alunos
                .Where(n => n.AlunoId == alunoId && n.Data == data) // Filtra por ID e data
                .OrderBy(n => n.Data) // Ordena pela data (pode não ser necessário se você espera um único aluno)
                .FirstOrDefaultAsync(); // Retorna o primeiro aluno ou null se não encontrado
        }
    }
}
