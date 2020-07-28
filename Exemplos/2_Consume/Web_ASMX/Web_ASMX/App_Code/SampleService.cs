using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

/// <summary>
/// Descrição resumida de SampleService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// Para permitir que esse serviço da web seja chamado a partir do script, usando ASP.NET AJAX, remova os comentários da linha a seguir. 
// [System.Web.Script.Services.ScriptService]
public class SampleService : System.Web.Services.WebService
{

    public SampleService()
    {

        //Remova os comentários da linha a seguir se usar componentes designados 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld()
    {
        return "Olá, Mundo";
    }

    [WebMethod]
    public int Add(int a, int b)
    {
        return a + b;
    }
    [WebMethod]
    public int Subtract(int a, int b)
    {
        return a - b;
    }

}
