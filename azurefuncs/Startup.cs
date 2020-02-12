using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Glasswall.Redact.Api;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Glasswall.Redact.Api
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddSingleton(typeof(IJsonToXmlConverter<>), typeof(JsonToXmlConverter<>));
        }
    }
}