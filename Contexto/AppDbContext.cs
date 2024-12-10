using api_db.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Identidade;
using ProjetoRecepcao.Identidade.Gestores;


namespace ProjetoRecepcao.Contexto
{
  public class AppDbContext : IdentityDbContext
  {
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // DbSet para Aluno, AlunoHorario
    public DbSet<Cadastro> cadastros { get; set; }
    public DbSet<Login> logins { get; set; }
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<PlanilhaReposicao> PlanilhaReposicaos { get; set; }
    public DbSet<alunosreposicao> alunosreposicaos { get; set; }
    public DbSet<criacaoplanalunos> criacaoplanalunos { get; set; }




    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      base.OnModelCreating(modelBuilder);

      // Configuração da entidade Aluno
      modelBuilder.Entity<Aluno>();

      // Configuração da entidade alunosreposicao
      modelBuilder.Entity<alunosreposicao>();
        

      // Configuração da entidade criacaoplanalunos
      modelBuilder.Entity<criacaoplanalunos>();

      // Configuração da entidade PlanilhaReposicao
      modelBuilder.Entity<PlanilhaReposicao>();
    }



  }
}


