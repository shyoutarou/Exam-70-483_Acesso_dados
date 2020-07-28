using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WebIQuery
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "IQueryService" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface IQueryService
    {
        [OperationContract]
        [WebGet]
        string DoWork();
    }
}
