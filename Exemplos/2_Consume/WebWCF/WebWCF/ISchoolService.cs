using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "ISchoolService" no arquivo de código e configuração ao mesmo tempo.
[ServiceContract]
public interface ISchoolService
{
    [OperationContract]
    [WebGet]
    string echoWithGet(string param1);

    [OperationContract]
    [WebInvoke(Method = "GET")]
    string echoWithPost(string param1);

    [OperationContract]
    [WebGet]
    string DoWork();

    [OperationContract]
    [WebGet]
    List<StudentData> GetStudents();

    [OperationContract]
    [WebGet]
    StudentData GetStudent(int ID);

    [OperationContract]
    [WebInvoke(Method = "POST")]
    void CreateStudent(Student student);

    [OperationContract]
    [WebInvoke(Method = "POST")]
    void UpdateStudent(StudentData student);

    [OperationContract]
    //[WebInvoke(Method = "DELETE")]
    [WebInvoke(Method = "POST")]
    void DeleteStudent(int StudentID);
}
}
