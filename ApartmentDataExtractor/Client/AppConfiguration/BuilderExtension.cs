using ApartmentPriceParser.Common.Models;
using Client.DataProviders.Clients;
using Client.DataProviders.Options;
using Client.DataProviders;
using Microsoft.Extensions.Options;

namespace Client.AppConfiguration
{
    public static class BuilderExtension
    {
        public static WebApplicationBuilder ConfigureApp(this WebApplicationBuilder builder)
        {
            var avitoKeyValuePairFactory = builder.Services.RegisterAvitoDataProvider(builder.Configuration);
            var cianKeyValuePairFactory = builder.Services.RegisterCianDataProvider(builder.Configuration);
            builder.Services.AddSingleton(p => new Dictionary<ProviderType, IDataProvider>(new List<KeyValuePair<ProviderType, IDataProvider>>()
                {
                avitoKeyValuePairFactory(p),
                cianKeyValuePairFactory(p)
            }));
            builder.Services.AddHostedService<WorkerService>();
            builder.Services.AddSingleton<IBackendDataManager, BackendDataManager>();
            return builder;
        }
    }
}
