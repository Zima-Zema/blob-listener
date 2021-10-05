using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Company.Aptar
{
    public static class BlobTrigger
    {
        // Create a single, static HttpClient
        private static HttpClient httpClient = new HttpClient();
        
        [FunctionName("BlobTrigger")]
        public static async Task Run([BlobTrigger("blob/{name}", Connection = "aptar_STORAGE")] Stream myBlob, string name, ILogger log)
        {
            var json = JsonConvert.SerializeObject(new { name= name, length = myBlob.Length});
            StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://aptar-blob-logger.azurewebsites.net/api/SendMessage", data);
            log.LogInformation($"C# Blob trigger function Processed blob\n Name:{name} \n Queue Response: {response.Content.ToString()}");
            
        }
    }
}
