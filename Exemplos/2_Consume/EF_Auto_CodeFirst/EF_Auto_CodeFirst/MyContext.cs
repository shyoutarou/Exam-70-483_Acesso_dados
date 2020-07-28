using System.Data.Entity;

namespace EF_Auto_CodeFirst
{
    public class MyContext : DbContext
    {
        public MyContext() : base("MyContextDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<MyContext,
        EF_Auto_CodeFirst.Migrations.Configuration>("MyContextDB"));
        }
        public virtual DbSet<Curso> Cursos { get; set; }
        public virtual DbSet<Inscricao> Inscricoes { get; set; }
        public virtual DbSet<Aluno> Alunos { get; set; }
        public virtual DbSet<AlunoLogin> AlunoLogins { get; set; }
    }
}
