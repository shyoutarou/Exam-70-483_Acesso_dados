using System;
using System.Linq;

namespace EF_Database_First
{
    class Program
    {
        static void Main(string[] args)
        {
            //Database object

            using (SchoolDB db = new SchoolDB())
            {
                //LINQ query to get students
                var students = (from p in db.Student
                                select p).ToList();
                foreach (var student in students)
                {
                    Console.WriteLine("ID is: " + student.StudentID);
                    Console.WriteLine("Name is: " + student.StudentName);
                }

                var classes = from c in db.Student
                              join p in db.Class
                              on c.StudentID equals p.StudentID
                              select p;
                foreach (var clas in classes)
                {
                    Console.WriteLine(string.Format("StudentName: {0}, ClassName: {1}",
                        clas.Student.StudentName, clas.ClassName));
                }


                Student st = new Student();
                st.StudentName = "Mubashar Rafique";
                db.Student.Add(st);
                db.SaveChanges();
                Console.WriteLine("Student Added!");

                //Find specific Studnet
                var std = (from p in db.Student
                           where p.StudentName == "Mubashar Rafique"
                           select p).FirstOrDefault();

                if (std != null)//if student is found
                {
                    //update the record.
                    std.StudentName = "Updated Name";
                    db.SaveChanges();
                    Console.WriteLine("Student Updated!");
                }

                if (std != null)//if student is found
                {
                    //delete the record
                    db.Student.Remove(std);
                    db.SaveChanges();
                    Console.WriteLine("Student Removed!");
                }

                var retornosp = db.PersonInsert("Alceu", "Valenca");
                Console.WriteLine(retornosp); //-1

            }

            Console.ReadKey();
        }
    }
}
