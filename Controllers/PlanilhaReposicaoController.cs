using Microsoft.AspNetCore.Mvc;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Servicos;

namespace ProjetoRecepcao.Controllers
{
    public class PlanilhaReposicaoController : Controller
    {

        private readonly IReposicaoService _reposicaoService;

        // Construtor combinado
        public PlanilhaReposicaoController(IReposicaoService reposicaoService)
        {
            _reposicaoService = reposicaoService;

        }
        [HttpGet("/reposicao/dia")]
        public async Task<ActionResult<IAsyncEnumerable<PlanilhaReposicao>>> GetReposicaoByData([FromQuery] DateOnly data)
        {
            try
            {
                var reposicaoDia = await _reposicaoService.GetReposicaoByData(data);
                if (!reposicaoDia.Any())
                {
                    return NotFound($"Não existem alunos com o nome {data}");
                }
                return Ok(reposicaoDia);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter alunos por nome.");
            }
        }
    }
}
