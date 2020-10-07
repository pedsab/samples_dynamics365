using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Tooling.Connector;

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            var crmConnectionString = ConfigurationManager.ConnectionStrings["crmConnectionString"];

            var crmService = new CrmServiceClient(crmConnectionString.ConnectionString);

            if (crmService.IsReady)
            {
                // ready

                // Retrieve Multiple

                // SELECT TOP 5 * FROM contact WHERE fullname = 'Paulo'

                var query = new QueryExpression("contact");
                query.ColumnSet.AllColumns = true;
                query.TopCount = 5;
                //query.Criteria.AddCondition("fullname", ConditionOperator.Equal, "Paulo");

                var ec = crmService.RetrieveMultiple(query);

                foreach (Entity contact in ec.Entities)
                {
                    Console.WriteLine($"Nome completo: {contact.GetAttributeValue<string>("fullname")}");
                    Console.WriteLine($"Nascimento: {contact.GetAttributeValue<DateTime>("birthdate")}");
                }

                // Create

                var entity = new Entity("contact");
                entity["fullname"] = "Teste";
                entity["birthdate"] = DateTime.UtcNow;

                var crmId = crmService.Create(entity);

                Console.ReadKey();
            }
            else
            {
                // not ready
            }
        }
    }
}
