using ApartmentPriceParser.Common.Models;
using ApartmentPriceParser.Common.Models.Providers;
using Client.DataProviders.Clients;
using Client.DataProviders.Options;
using HtmlAgilityPack;
using Microsoft.Extensions.Options;

namespace Client.DataProviders
{
    public class AvitoDataProvider : IDataProvider
    {
        private readonly IAvitoClient avitoDataExtarctor;
        private readonly AvitoProviderOptions avitoProviderOptions;
        public AvitoDataProvider(IAvitoClient avitoDataExtarctor, IOptions<AvitoProviderOptions> options)
        {
            this.avitoDataExtarctor = avitoDataExtarctor;
            this.avitoProviderOptions = options.Value;
        }
        public async Task<ApartmentData> GetApartmentData(object request)
        {
            var definition = (AvitoApartmentDefinition)request;
            var apartmentHtml = await avitoDataExtarctor.GetApartmentData(definition.City, definition.AvitoId);
            var doc = new HtmlDocument();
            doc.LoadHtml(apartmentHtml);
            var nameString = doc.DocumentNode.SelectSingleNode(avitoProviderOptions.NameXpath)?.InnerText;
            var priceString = doc.DocumentNode.SelectSingleNode(avitoProviderOptions.PriceXpath)?.Attributes
                .FirstOrDefault(x => x.Name == avitoProviderOptions.PriceAttribute)?.Value;
            var price = Convert.ToInt32(priceString);


            return new ApartmentData()
            {
                Id = definition.Id,
                Name = nameString,
                Price = price
            };
        }
    }
}
