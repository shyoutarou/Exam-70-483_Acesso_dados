using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_Auto_CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyContext db = new MyContext())
            {
                //Aluno st = new Aluno();
                //st.EnrollmentDate = DateTime.Now;
                //st.FirstMidName = "Mubashar Rafique";
                //st.LastName = "Silva";
                //db.Alunos.Add(st);
                //db.SaveChanges();
                //Console.WriteLine("Aluno Added!");

                var alunos = db.Alunos;
                foreach (var item in alunos)
                {
                    Console.WriteLine(item.LastName);
                }
            }

            Console.WriteLine(" Banco de dados e tabelas cridas com sucesso");
            Console.ReadKey();
        }
    }
}
