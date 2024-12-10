using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Identidade.Gestores;
using ProjetoRecepcao.Servicos.Interfaces;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoRecepcao.Controllers
{
  //[authorize]
  [ApiController]
  [Route("api/[controller]")]
 
  public class LoginController : ControllerBase
  {
    private readonly ILoginService _loginService;
    private readonly ILogger<LoginController> _logger;
    private readonly IConfiguration _configuration;

    public LoginController(ILoginService loginService)
    {
      _loginService = loginService ?? throw new ArgumentNullException(nameof(loginService));
    }





    [HttpPost("autenticar")]
    public async Task<IActionResult> AuthenticateAsync([FromBody] Login model)
    {
      if (model == null || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Senha))
      {
        return BadRequest("Email e senha são obrigatórios.");
      }

      // Autenticar o usuário no banco
      var user = await _loginService.AuthenticateByEmailAsync(model.Email, model.Senha);

      if (user == null)
      {
        return Unauthorized(new { Message = "Credenciais inválidas." });
      }

      // Gere o token JWT
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.ASCII.GetBytes("sua-chave-super-secreta-com-no-minimo-32-caracteres");

      var tokenDescriptor = new SecurityTokenDescriptor
      {
        Subject = new ClaimsIdentity(new[]
          {
            new Claim(ClaimTypes.Name, user.Nome), // Obtém o Nome do objeto "user"
            new Claim(ClaimTypes.Role, "User")     // Pode personalizar o role conforme necessário
        }),
        Expires = DateTime.UtcNow.AddHours(1),
        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
      };

      var token = tokenHandler.CreateToken(tokenDescriptor);

      return Ok(new
      {
        Token = tokenHandler.WriteToken(token),
        Expiration = tokenDescriptor.Expires,
        Message = "Autenticação bem-sucedida!"

        
      });
      
    }

    // GET: api/login
    [HttpGet("/listar/logins")]
    public async Task<ActionResult<IEnumerable<Login>>> GetAllLogins()
    {
      try
      {
        var logins = await _loginService.GetAllLoginsAsync();
        return Ok(logins);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter logins: {ex.Message}");
      }
    }

    // GET: api/login/{id}
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Login>> GetLoginById(Guid id)
    {
      try
      {
        var login = await _loginService.GetLoginByIdAsync(id);
        if (login == null)
        {
          return NotFound($"Login com ID {id} não encontrado.");
        }
        return Ok(login);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao obter login: {ex.Message}");
      }
    }

    // GET: api/login/search?nome={nome}
    [HttpGet("search/logins  ")]
    public async Task<ActionResult<IEnumerable<Login>>> GetLoginsByNome([FromQuery] string nome)
    {
      try
      {
        var logins = await _loginService.GetLoginsByNomeAsync(nome);
        if (!logins.Any())
        {
          return NotFound($"Nenhum login encontrado com o nome '{nome}'.");
        }
        return Ok(logins);
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao buscar logins por nome: {ex.Message}");
      }
    }

    //// POST: api/login
    //[HttpPost]
    //public async Task<ActionResult<Login>> CreateLogin([FromBody] Login login)
    //{
    //  if (login == null)
    //  {
    //    return BadRequest("Dados do login não fornecidos.");
    //  }

    //  try
    //  {
    //    await _loginService.CreateLoginAsync(login);
    //    return CreatedAtAction(nameof(GetLoginById), new { id = login.Id }, login);
    //  }
    //  catch (Exception ex)
    //  {
    //    return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao criar login: {ex.Message}");
    //  }
    //}

    // PUT: api/login/{id}
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateLogin(Guid id, [FromBody] Login login)
    {
      if (login == null || id != login.Id)
      {
        return BadRequest("Dados inconsistentes ou não fornecidos.");
      }

      try
      {
        var existingLogin = await _loginService.GetLoginByIdAsync(id);
        if (existingLogin == null)
        {
          return NotFound($"Login com ID {id} não encontrado.");
        }

        await _loginService.UpdateLoginAsync(login);
        return Ok("Login atualizado com sucesso.");
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao atualizar login: {ex.Message}");
      }
    }

    // DELETE: api/login/{id}
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteLogin(Guid id)
    {
      try
      {
        var login = await _loginService.GetLoginByIdAsync(id);
        if (login == null)
        {
          return NotFound($"Login com ID {id} não encontrado.");
        }

        await _loginService.DeleteLoginAsync(id);
        return Ok($"Login com ID {id} excluído com sucesso.");
      }
      catch (Exception ex)
      {
        return StatusCode(StatusCodes.Status500InternalServerError, $"Erro ao excluir login: {ex.Message}");
      }
    }
  }
}
