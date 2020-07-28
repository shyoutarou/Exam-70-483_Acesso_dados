using System;
using WCF_Teste.ServiceReference1;

namespace WCF_Teste
{

    class Program
    {
        static void Main(string[] args)
        {

            using (SchoolServiceClient client = new SchoolServiceClient())
            {
                var retorno = client.DoWork();
                Console.WriteLine(retorno); // Hell World

                var students = client.GetStudents();
                foreach (var student in students)
                {
                    Console.WriteLine("ID is: " + student.ID);
                    Console.WriteLine("Name is: " + student.Nome);
                }

                Student st = new Student();
                st.StudentName = "Mubashar Rafique";
                client.CreateStudent(st);
                Console.WriteLine("Student Added!");

                StudentData std = client.GetStudent(1);
                if (std != null)
                {
                    std.Nome = "Updated Name";
                    client.UpdateStudent(std);
                    Console.WriteLine("Student Updated!");

                    client.DeleteStudent(3);
                    Console.WriteLine("Student Removed!");
                }
            };

            Console.ReadKey();
        }
    }
}
