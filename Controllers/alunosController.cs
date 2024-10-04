using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Conversores;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Servicos;
using System.Drawing;
using System;

namespace ProjetoRecepcao.Controllers
{
    [Route("api/alunos")]
    [ApiController]
    public class alunosController : ControllerBase
    {
        private readonly IAlunoService _alunoService;
       

        // Construtor combinado
        public alunosController(IAlunoService alunoService)
        {
            _alunoService = alunoService;
            
        }

        [HttpGet("listar")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAluno()
        {
            try
            {
                var alunos = await _alunoService.GetAluno();
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos.");
            }
        }

        [HttpGet("pesquisarnome")]
        public async Task<ActionResult<IAsyncEnumerable<Aluno>>> GetAlunoByNome([FromQuery] string nome)
        {
            try
            {
                var alunos = await _alunoService.GetAlunoByNome(nome);
                if (!alunos.Any())
                {
                    return NotFound($"Não existem alunos com o nome {nome}");
                }
                return Ok(alunos);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos por nome.");
            }
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Aluno>> GetAluno(Guid id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                {
                    return NotFound($"Não existe aluno com o ID {id}");
                }
                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro: {ex.Message}");
            }
        }

        [HttpPost("criar")]
        public async Task<ActionResult<Aluno>> Create(Aluno aluno)
        {
            try
            {
                if (aluno == null)
                {
                    return BadRequest("Dados do aluno não fornecidos.");
                }

                await _alunoService.CreateAluno(aluno);

                // Use o nome da rota definida no método "GetAlunoById"
                return CreatedAtRoute("GetAlunoById", new { id = aluno.AlunoId }, aluno);
                
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar aluno: {ex.Message}");
            }
        }

        [HttpGet("pesquisar/{id}", Name = "GetAlunoById")]
        public async Task<ActionResult<Aluno>> GetAlunoById(Guid id)
        {
            try
            {
                var aluno = await _alunoService.GetAlunoById(id);

                if (aluno == null)
                {
                    return NotFound($"Não existem alunos com o ID: {id}");
                }

                return Ok(aluno);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter aluno por ID: {ex.Message}");
            }
        }


        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Edit(Guid id, [FromBody] Aluno aluno)
        {
            if (aluno == null)
            {
                return BadRequest("Aluno não pode ser nulo.");
            }

            if (aluno.AlunoId != id)
            {
                return BadRequest("O ID do aluno não corresponde ao ID fornecido na URL.");
            }

            try
            {
                // Verifique a existência do aluno antes de atualizar
                var existingAluno = await _alunoService.GetAluno(id);
                if (existingAluno == null)
                {
                    return NotFound("Aluno não encontrado.");
                }

                try
                {
                    await _alunoService.UpdateAluno(aluno);
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

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            try
            {
                var aluno = await _alunoService.GetAluno(id);
                if (aluno == null)
                {
                    return NotFound($"Aluno com ID {id} não encontrado.");
                }
                await _alunoService.DeleteAluno(aluno);
                return Ok($"Aluno com ID {id} excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir aluno: {ex.Message}");
            }
        }
        [HttpGet("pesquisar/data/{data}/horario/{horario}")]
        public async Task<ActionResult<IAsyncEnumerable<PlanilhaReposicao>>> GetAlunoByDataAndHorario(DateOnly data, string horario)
        {
            try
            {
                // Supondo que você tenha um método no seu serviço que recebe data e horário
                var reposicaoDia = await _alunoService.GetAlunoByData(data, horario);

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








    }
}
