using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Servicos;
using System.Xml.Linq;

namespace ProjetoRecepcao.Controllers
{
    [Route("api/alunosreposicao")]
    public class PlanilhaReposicaoController : Controller
    {

        private readonly IReposicaoService _reposicaoService;

        // Construtor combinado
        public PlanilhaReposicaoController(IReposicaoService reposicaoService)
        {
            _reposicaoService = reposicaoService;

        }
        [HttpGet("listar/data/{data}/horario/{horario}")]
        public async Task<ActionResult<IAsyncEnumerable<PlanilhaReposicao>>> GetAlunoByDataHorario(DateOnly data, string horario)
        {
            try
            {
                // Supondo que você tenha um método no seu serviço que recebe data e horário
                var reposicaoDia = await _reposicaoService.GetAlunoByDataHorario(data, horario);

                if (!reposicaoDia.Any())
                {
                    return NotFound($"Não existem alunos com a data: {data} e horário: {horario}");
                }

                return Ok(reposicaoDia);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter alunos por data e horário: {ex.Message}");
            }
        }

        [HttpGet("listarrepo")]
        public async Task<ActionResult<IAsyncEnumerable<PlanilhaReposicao>>> GetReposicao()
        {
            try
            {
                var alunosReposicao = await _reposicaoService.GetReposicao();
                return Ok(alunosReposicao);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos.");
            }
        }

        

        [HttpGet("/listarunirep{id:guid}")]
        public async Task<ActionResult<PlanilhaReposicao>> GetReposicao(Guid id)
        {
            try
            {
                var alunosReposicao = await _reposicaoService.GetReposicao(id);
                if (alunosReposicao == null)
                {
                    return NotFound($"Não existe aluno com o ID {id}");
                }
                return Ok(alunosReposicao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        [HttpPost("criarrep")]
        public async Task<ActionResult<PlanilhaReposicao>> CreatePlanilhaReposicao([FromBody]PlanilhaReposicao planilhaReposicao)
        {
            try
            {
                if (planilhaReposicao == null)
                {
                    return BadRequest("Dados do aluno não fornecidos.");
                }

                await _reposicaoService.CreatePlanilhaReposicao(planilhaReposicao);

                // Use o nome da rota definida no método "GetAlunoById"
                return CreatedAtRoute("GetReposicaoById", new { id = planilhaReposicao.AlunoId }, planilhaReposicao);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar aluno: {ex.Message}");
            }
        }

        [HttpGet("pesquisar/{id}", Name = "GetReposicaoById")]
        public async Task<ActionResult<PlanilhaReposicao>> GetReposicaoById(Guid id)
        {
            try
            {
                var alunoReposicao = await _reposicaoService.GetReposicaoById(id);

                if (alunoReposicao == null)
                {
                    return NotFound($"Não existem alunos com o ID: {id}");
                }

                return Ok(alunoReposicao);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter aluno por ID: {ex.Message}");
            }
        }



        [HttpPut("atualizarrepo/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] PlanilhaReposicao planilhaReposicao)
        {
            if (planilhaReposicao == null)
            {
                return BadRequest("Aluno não pode ser nulo.");
            }

            if (planilhaReposicao.AlunoId != id)
            {
                return BadRequest("O ID do aluno não corresponde ao ID fornecido na URL.");
            }

            try
            {
                // Verifique a existência do aluno antes de atualizar
                var existingAlunoReposicao = await _reposicaoService.GetReposicao(id);
                if (existingAlunoReposicao == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                try
                {
                    await _reposicaoService.UpdatePlanilhaReposicao(planilhaReposicao);
                    return Ok("Aluno Atualizado com Sucesso");
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Conflict("O aluno foi modificado ou excluído por outro usuário. Por favor, recarregue os dados e tente novamente.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar aluno: {ex.Message}");
            }
        }

        [HttpDelete("deletarrepo/{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var alunoReposicao = await _reposicaoService.GetReposicao(id);
                if (alunoReposicao == null)
                {
                    return NotFound($"Aluno com ID {id} não encontrado.");
                }
                await _reposicaoService.DeletePlanilhaReposicao(alunoReposicao);
                return Ok($"Aluno com ID {id} excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir aluno: {ex.Message}");
            }
        }
    }
}
