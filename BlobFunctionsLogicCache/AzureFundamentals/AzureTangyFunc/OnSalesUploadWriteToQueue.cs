using AzureTangyFunc.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Storage;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureTangyFunc
{
    public class OnSalesUploadWriteToQueue
    {
        private readonly ILogger<OnSalesUploadWriteToQueue> _logger;

        public OnSalesUploadWriteToQueue(ILogger<OnSalesUploadWriteToQueue> logger)
        {
            _logger = logger;
        }

        [Function("OnSalesUploadWriteToQueue")]
        [QueueOutput("SalesRequestInBound")]
        public async Task<SalesRequest> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");

            
            
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();

            var salesRequest = JsonConvert.DeserializeObject<SalesRequest>(requestBody);

            return salesRequest ?? new SalesRequest();
        }
    }
}
