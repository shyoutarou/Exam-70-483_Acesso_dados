using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Auto_CodeFirst
{
    public class Inscricao
    {
        public int InscricaoID { get; set; }
        public int CursoID { get; set; }
        public int AlunoID { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
