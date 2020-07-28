using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EF_CodeBasedFirst
{
    public class Aluno
    {
        [Key]
        public int ID { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int Idade { get; set; }
        public virtual ICollection<Inscricao> Inscricoes { get; set; }
    }
}
