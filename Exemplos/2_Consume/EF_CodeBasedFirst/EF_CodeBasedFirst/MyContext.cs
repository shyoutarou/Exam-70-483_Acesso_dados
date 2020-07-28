using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeBasedFirst
{
    public class MyContext : DbContext
    {
        public MyContext() : base("SchoolDB")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<
                MyContext, EF_CodeBasedFirst.Migrations.Configuration>());
        }

        public virtual DbSet<Curso> Curso { get; set; }
        public virtual DbSet<Inscricao> Inscricao { get; set; }
        public virtual DbSet<Aluno> Aluno { get; set; }
    }
}
