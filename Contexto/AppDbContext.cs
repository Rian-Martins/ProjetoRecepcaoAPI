using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ProjetoRecepcao.Identidade;

namespace ProjetoRecepcao.Contexto
{
    public class AppDbContext : IdentityDbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        // DbSet para Aluno, AlunoHorario
        public DbSet<Aluno> Alunos { get; set; }
        /*public DbSet<AlunoHorario> AlunoHorarios { get; set; */



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Aluno>();
        }
    }
    
}
    //protected override void OnModelCreating(ModelBuilder modelBuilder)
    //{


    //base.OnModelCreating(modelBuilder);
    //modelBuilder.Entity<Alunos>();

    ////// Configuração do relacionamento entre Aluno e AlunoHorario
    ////modelBuilder.Entity<Aluno>()
    ////    .HasMany(a => a.AlunoHorarios)
    ////    .WithOne(h => h.Aluno)
    ////    .HasForeignKey(h => h.Id)
    ////    .OnDelete(DeleteBehavior.Cascade); // Exclui os AlunoHorario associados ao excluir o Aluno



    //// Adicionando restrições opcionais (se necessário)
    ////modelBuilder.Entity<Aluno>()
    ////        .Property(a => a.Nome)
    ////        .IsRequired() // Exemplo de campo obrigatório
    ////        .HasMaxLength(100); // Exemplo de tamanho máximo

    //    // Chamada para o base.OnModelCreating
    //    //base.OnModelCreating(modelBuilder);
    //}


