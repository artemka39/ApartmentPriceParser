using ApartmentPriceParser.Common.Models;
using Client.DataProviders.Clients;
using Client.DataProviders.Options;
using Client.DataProviders;
using Microsoft.Extensions.Options;
using Refit;
using static System.Net.Mime.MediaTypeNames;

namespace Client.AppConfiguration
{
    public static class ProviderExtensions
    {
        public static Func<IServiceProvider, KeyValuePair<ProviderType, IDataProvider>> RegisterAvitoDataProvider(
            this IServiceCollection services,
            ConfigurationManager builderConfiguration)
        {
            services
                .AddRefitClient<IAvitoClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builderConfiguration.GetValue<string>("Providers:Avito:BaseUrl")!));
            services.Configure<AvitoProviderOptions>(builderConfiguration.GetSection("Providers:Avito"));
            var avitoKeyValuePairFactory = (IServiceProvider p) =>
                new KeyValuePair<ProviderType, IDataProvider>(ProviderType.Avito,
                    new AvitoDataProvider(p.GetRequiredService<IAvitoClient>(),
                    p.GetRequiredService<IOptions<AvitoProviderOptions>>()));
            return avitoKeyValuePairFactory;
        }


        public static Func<IServiceProvider, KeyValuePair<ProviderType, IDataProvider>> RegisterCianDataProvider(
            this IServiceCollection services,
            ConfigurationManager builderConfiguration)
        {
            services
                .AddRefitClient<ICianClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builderConfiguration.GetValue<string>("Providers:Cian:BaseUrl")!));

            services.Configure<CianProviderOptions>(builderConfiguration.GetSection("Providers:Cian"));
            var cianKeyValuePairFactory = (IServiceProvider p) =>
                new KeyValuePair<ProviderType, IDataProvider>(ProviderType.Cian,
                    new CianDataProvider(p.GetRequiredService<ICianClient>(),
                    p.GetRequiredService<IOptions<CianProviderOptions>>()));
            return cianKeyValuePairFactory;
        }
    }
}
