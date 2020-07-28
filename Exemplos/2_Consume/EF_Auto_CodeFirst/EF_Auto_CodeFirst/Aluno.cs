using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF_Auto_CodeFirst
{
    public class Aluno
    {
        [Key]
        public int AlunoID { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}
