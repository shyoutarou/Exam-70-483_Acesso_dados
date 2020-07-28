using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Auto_CodeFirst
{
    public class Curso
    {
        public int CursoID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}
