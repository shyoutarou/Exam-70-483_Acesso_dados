using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Auto_CodeFirst
{
    public class AlunoLogin
    {
        [Key, ForeignKey("Aluno")]
        public int ID { get; set; }
        public string EmailID { get; set; }
        public string Password { get; set; }

        public virtual Aluno Aluno { get; set; }
    }
}
