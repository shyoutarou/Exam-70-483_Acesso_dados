using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_CodeBasedFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (MyContext db = new MyContext())
            {
                Aluno st = new Aluno();
                st.EnrollmentDate = DateTime.Now;
                st.FirstName = "Sasha";
                st.LastName = "Meneghel";
                db.Aluno.Add(st);
                db.SaveChanges();
                Console.WriteLine("Aluno Added!");

                var alunos = db.Aluno;
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
