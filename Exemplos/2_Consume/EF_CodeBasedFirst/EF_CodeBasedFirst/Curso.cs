using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF_CodeBasedFirst
{
    public class Curso
    {
        [Key]
        public int CursoID { get; set; }
        public string Title { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}
