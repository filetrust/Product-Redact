using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Glasswall.Redact.Api
{
    public class RedactFile
    {
        private readonly IJsonToXmlConverter<List<Setting>> _jsonToXmlConverter;

        public RedactFile(IJsonToXmlConverter<List<Setting>> jsonToXmlConverter)
        {
            if (jsonToXmlConverter != null) _jsonToXmlConverter = jsonToXmlConverter;
        }

        [FunctionName("RedactFile")]
        public async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            if (req.Form.Files.Count < 1)
                return new BadRequestObjectResult("You must specify a file and an xml");
            
            var fileArg = req.Form.Files[0];

            req.Form.TryGetValue("redactJson", out var jsonArg);
            if(string.IsNullOrWhiteSpace(jsonArg))
                return new BadRequestObjectResult("redactJson is invalid");

            var xml = _jsonToXmlConverter.Convert(jsonArg);

            if (string.IsNullOrWhiteSpace(xml))
                return new BadRequestObjectResult("Unable to build XML");

            return new OkObjectResult("Received a file to redact stuff in: " + fileArg.FileName + " and JSON: " + jsonArg + " we built the XML " + xml);
        }
    }
}