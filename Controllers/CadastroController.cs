using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoRecepcao.Identidade.Gestores;
using ProjetoRecepcao.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoRecepcao.Controllers
{
  //[Authorize]
  [ApiController]
  [Route("api/[controller]")]

  public class CadastroController : ControllerBase
  {
    private readonly ICadastroService _cadastroService;

    public CadastroController(ICadastroService cadastroService)
    {
      _cadastroService = cadastroService ?? throw new ArgumentNullException(nameof(cadastroService));
    }

    // GET: api/cadastro
    [HttpGet("/listar")]
    public async Task<ActionResult<IEnumerable<Cadastro>>> GetAllCadastros()
    {
      try
      {
        var cadastros = await _cadastroService.GetAllCadastrosAsync();
        return Ok(cadastros);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro ao obter cadastros: {ex.Message}");
      }
    }

    // GET: api/cadastro/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Cadastro>> GetCadastroById(Guid id)
    {
      try
      {
        var cadastro = await _cadastroService.GetCadastroByIdAsync(id);
        if (cadastro == null)
        {
          return NotFound($"Cadastro com ID {id} não encontrado.");
        }
        return Ok(cadastro);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro ao obter cadastro: {ex.Message}");
      }
    }

    // POST: api/cadastro
    [HttpPost("/cadastro")]
    public async Task<ActionResult> CreateCadastro([FromBody] Cadastro cadastro)
    {
      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      try
      {
        await _cadastroService.CreateCadastroAsync(cadastro);
        return CreatedAtAction(nameof(GetCadastroById), new { id = cadastro.Id }, cadastro);
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro ao criar cadastro: {ex.Message}");
      }
    }

    // PUT: api/cadastro/{id}
    [HttpPut("atualizar/{id:guid}")]
    public async Task<ActionResult> UpdateCadastro(Guid id, [FromBody] Cadastro cadastro)
    {
      if (cadastro == null || id != cadastro.Id)
      {
        return BadRequest("Dados inconsistentes ou não fornecidos.");
      }

      try
      {
        var cadastroExistente = await _cadastroService.GetCadastroByIdAsync(id);
        if (cadastroExistente == null)
        {
          return NotFound($"Cadastro com ID {id} não encontrado.");
        }

        await _cadastroService.UpdateCadastroAsync(cadastro);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro ao atualizar cadastro: {ex.Message}");
      }
    }

    // DELETE: api/cadastro/{id}
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteCadastro(Guid id)
    {
      try
      {
        var cadastro = await _cadastroService.GetCadastroByIdAsync(id);
        if (cadastro == null)
        {
          return NotFound($"Cadastro com ID {id} não encontrado.");
        }

        await _cadastroService.DeleteCadastroAsync(id);
        return NoContent();
      }
      catch (Exception ex)
      {
        return StatusCode(500, $"Erro ao deletar cadastro: {ex.Message}");
      }
    }
  }
}
