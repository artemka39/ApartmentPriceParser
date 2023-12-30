using ApartmentPriceParser.Backend.Database;
using ApartmentPriceParser.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceParser.Backend.Services
{
    public class ApartmentService
    {
        private readonly Dictionary<ProviderType, IApartmentDefifnitionRepository> repositories;
        public ApartmentService(Dictionary<ProviderType, IApartmentDefifnitionRepository> repositories)
        {
            this.repositories = repositories;
        }
        public async Task<Dictionary<ProviderType, List<object>>> GetApartmentDefinitions()
        {
            var result = new Dictionary<ProviderType, List<object>>();
            foreach (var providerType in Enum.GetValues<ProviderType>())
            {
                var definitions = await repositories[providerType].GetAll();
                result.Add(providerType, definitions);
            }
            return result;
        }
    }
}
