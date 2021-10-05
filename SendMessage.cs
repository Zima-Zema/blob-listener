using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.WebJobs.ServiceBus;
using System.Text;
//TBC: move this functions to a seperate deployment 
namespace Company.Aptar
{
    public static class SendMessage
    {
        [FunctionName("SendMessage")]
        [return: ServiceBus("blob_queue", EntityType.Queue, Connection = "ServiceBusConnection")]
        public static async Task<string> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("SendMessage function requested");
            string body = string.Empty;
            using (var reader = new StreamReader(req.Body, Encoding.UTF8))
            {
                body = await reader.ReadToEndAsync();
                log.LogInformation($"Message body : {body}");
            }
            log.LogInformation($"SendMessage processed.");
            return body;
        }
    }
}
