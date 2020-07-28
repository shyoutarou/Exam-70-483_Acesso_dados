using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;

namespace WebWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "SchoolService" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione SchoolService.svc ou SchoolService.svc.cs no Gerenciador de Soluções e inicie a depuração.
public class SchoolService : ISchoolService
{
    public string echoWithGet(string s)
    {
        return "Get: " + s;
    }

    public string echoWithPost(string s)
    {
        return "Post: " + s;
    }

    public string DoWork()
    {
        return "Hell World";
    }

    public List<StudentData> GetStudents()
    {
        var lista = new List<StudentData>();

        using (SchoolDB db = new SchoolDB())
        {
            //LINQ query to get students
            var listaDB = (from p in db.Student
                            select p).ToList();

            foreach (var item in listaDB)
            {
                lista.Add(new StudentData() { ID = item.StudentID, Name = item.StudentName });
            }

        }

        return lista;
    }

    public StudentData GetStudent(int StudentID)
    {
        var student = new StudentData();

        using (SchoolDB db = new SchoolDB())
        {
            var studentDB = db.Student.Find(StudentID);

            student.ID = studentDB.StudentID;
            student.Name = studentDB.StudentName;
        }

        return student;
    }

    public void CreateStudent(Student student)
    {
        using (SchoolDB db = new SchoolDB())
        {
            db.Student.Add(student);
            db.SaveChanges();
        }
    }

    public void UpdateStudent(StudentData student)
    {
        using (SchoolDB db = new SchoolDB())
        {
            var EditedObj = db.Student.Find(student.ID);

            if (EditedObj != null)//if student is found
            {
                EditedObj.StudentName = student.Name;
                db.SaveChanges();
            }
        }
    }

    public void DeleteStudent(int StudentID)
    {
        using (SchoolDB db = new SchoolDB())
        {
            var EditedObj = db.Student.Find(StudentID);

            if (EditedObj != null)
            {
                var classes = db.Class.Where(x => x.StudentID == StudentID);

                foreach (var classe in classes)
                {
                    classe.StudentID = null;
                }

                db.Student.Remove(EditedObj);
                db.SaveChanges();
            }
        }
    }
}
}
