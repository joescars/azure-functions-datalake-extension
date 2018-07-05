using System.IO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.DataLake;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.WebJobs.Host;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DataLakeExtensionSamples
{
    public static class OutputFromHttp
    {
        [FunctionName("OutputFromHttp")]
        public static async Task<IActionResult> RunAsync([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequest req,
            [DataLakeStore(AccountFQDN = "%fqdn%", ApplicationId = "%applicationid%", ClientSecret = "%clientsecret%", TenantID = "%tentantid%")]IAsyncCollector<DataLakeStoreOutput> items,
            TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            await items.AddAsync(new DataLakeStoreOutput()
            {
                FileName = "/mydata/" + Guid.NewGuid().ToString() + ".txt",
                FileStream = req.Body
            });

            return new OkResult();
        }
    }
}