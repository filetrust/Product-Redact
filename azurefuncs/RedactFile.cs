using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Glasswall.Redact.Api
{
    public static class RedactFile
    {
        [FunctionName("RedactFile")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (req.Form.Files.Count <= 1)
                return new BadRequestObjectResult("You must specify a file and an xml");
            
            var fileArg = req.Form.Files[0];
            req.Form.TryGetValue("RedactJson", out var jsonArg);

            return new OkObjectResult("Received a file to redact stuff in: " + fileArg.FileName + " and an JSON: " + jsonArg);
        }
    }
}
