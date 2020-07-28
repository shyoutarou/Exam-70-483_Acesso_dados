using System.ComponentModel.DataAnnotations;

namespace EF_CodeBasedFirst
{
    public class Inscricao
    {
        [Key]
        public int ID { get; set; }
        public int CursoID { get; set; }
        public int AlunoID { get; set; }
        public virtual Curso Curso { get; set; }
        public virtual Aluno Aluno { get; set; }
    }
}
