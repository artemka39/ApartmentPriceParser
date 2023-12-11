using ApartmentPriceParser.Common.Models;
using ApartmentPriceParser.Common.Models.Providers;
using Client.DataProviders.Clients;
using Client.DataProviders.Options;
using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Refit;

namespace Client.DataProviders
{
    public class CianDataProvider : IDataProvider
    {
        private readonly ICianClient cianDataExtarctor;
        private readonly CianProviderOptions cianProviderOptions;
        public CianDataProvider(ICianClient cianDataExtarctor, IOptions<CianProviderOptions> options)
        {
            this.cianDataExtarctor = cianDataExtarctor;
            this.cianProviderOptions = options.Value;
        }
        public async Task<ApartmentData> GetApartmentData(object request)
        {
            var definition = (CianApartmentDefinition)request;
            var doc = new HtmlDocument();
            var apartmentHtmlResponse = await cianDataExtarctor.GetApartmentData(definition.CianId);
            var apartmentHtml = apartmentHtmlResponse.IsSuccessStatusCode ?
                await apartmentHtmlResponse.Content.ReadAsStringAsync() :
                File.ReadAllText(cianProviderOptions.SubstituteApartmentData);
            doc.LoadHtml(apartmentHtml);
            var nameString = doc.DocumentNode.SelectSingleNode(cianProviderOptions.NameXpath)?.InnerText;
            var priceString = doc.DocumentNode.SelectSingleNode(cianProviderOptions.PriceXpath)?.InnerText;
            var price = Convert.ToInt32(string.Concat(priceString.Where(c => char.IsDigit(c))));

            return new ApartmentData()
            {
                Id = definition.Id,
                Name = nameString,
                Price = price
            };
        }
    }
}
