using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OfficeOpenXml;
using OfficeOpenXml.Style;
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


        [HttpGet("export/excel")]
        public async Task<IActionResult> ExportToExcel()
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;  // Definindo o contexto de licença

            var alunos = await _reposicaoService.GetReposicao(); // Obtém os dados do banco

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Alunos");

                // Cabeçalhos
                worksheet.Cells[1, 1].Value = "AlunoId";
                worksheet.Cells[1, 2].Value = "Nome";
                worksheet.Cells[1, 3].Value = "Horario";
                worksheet.Cells[1, 4].Value = "Data";
                worksheet.Cells[1, 5].Value = "Professor";
                worksheet.Cells[1, 6].Value = "DiaSemana";

                // Popula os dados da planilha
                int row = 2;
                foreach (var aluno in alunos)
                {
                    worksheet.Cells[row, 1].Value = aluno.AlunoId;
                    worksheet.Cells[row, 2].Value = aluno.Nome;
                    worksheet.Cells[row, 3].Value = aluno.Horario;
                    worksheet.Cells[row, 4].Value = aluno.Data.ToString("yyyy-MM-dd");
                    worksheet.Cells[row, 5].Value = aluno.Professor;
                    worksheet.Cells[row, 6].Value = aluno.DiaSemana;
                    row++;
                }

                // Configurações de estilo da planilha (opcional)
                worksheet.Cells[1, 1, 1, 6].Style.Font.Bold = true;
                worksheet.Cells.AutoFitColumns();

                var excelBytes = package.GetAsByteArray();
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "alunos.xlsx");
            }
        }


        [HttpPost("import/excel")]
        public async Task<IActionResult> ImportFromExcel(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Nenhum arquivo foi enviado.");

            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);
                using (var package = new ExcelPackage(stream))
                {
                    var worksheet = package.Workbook.Worksheets.FirstOrDefault();
                    if (worksheet == null)
                        return BadRequest("O arquivo Excel está vazio.");

                    var rowCount = worksheet.Dimension.Rows;

                    var alunos = new List<PlanilhaReposicao>();

                    // Percorrer as linhas (começando na linha 2 para ignorar o cabeçalho)
                    for (int row = 2; row <= rowCount; row++)
                    {
                        var aluno = new PlanilhaReposicao
                        {
                            AlunoId = Guid.NewGuid(), // Gerar novo ID
                            Nome = worksheet.Cells[row, 2].Value?.ToString(),
                            Horario = worksheet.Cells[row, 3].Value?.ToString(),
                            Data = DateOnly.Parse(worksheet.Cells[row, 4].Value?.ToString()),
                            Professor = worksheet.Cells[row, 5].Value?.ToString(),
                            DiaSemana = worksheet.Cells[row, 6].Value?.ToString()
                        };
                        alunos.Add(aluno);
                    }

                    // Adicionar no banco de dados
                    await _reposicaoService.AddAlunosReposicao(alunos);
                }
            }

            return Ok("Dados importados com sucesso!");
        }


    }
}
