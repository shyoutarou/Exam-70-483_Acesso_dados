using System;
using System.Collections.Generic;
using System.Data.Services;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebIQuery
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "QueryService" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione QueryService.svc ou QueryService.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class QueryService : IQueryService // DataService<OrderItemData>
    {
        //// This method is called only once to initialize
        ////service-wide policies.
        //public static void InitializeService(IDataServiceConfiguration
        //                                        config)
        //{
        //    config.SetEntitySetAccessRule("Orders", EntitySetRights.All);
        //    config.SetEntitySetAccessRule("Items", EntitySetRights.All);


        //    //// Grant only the rights needed to support the client application.
        //    //config.SetEntitySetAccessRule("Orders", EntitySetRights.AllRead
        //    //        | EntitySetRights.WriteMerge
        //    //        | EntitySetRights.WriteReplace);
        //    //config.SetEntitySetAccessRule("Order_Details", EntitySetRights.AllRead
        //    //    | EntitySetRights.AllWrite);
        //    //config.SetEntitySetAccessRule("Customers", EntitySetRights.AllRead);
        //}

        public string DoWork()
        {
            return "Hello";
        }
    }
}
