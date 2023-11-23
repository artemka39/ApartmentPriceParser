using ApartmentPriceParser.Common.Models;
using ApartmentPriceParser.Common.Models.Providers;
using Client.DataProviders;
using Newtonsoft.Json;

namespace Client
{
    public interface IBackendDataManager
    {
        public void UpdateData(Dictionary<ProviderType, List<ApartmentData>> data);
        public Dictionary<ProviderType, List<object>> GetJobDefinitions();
    }
    public class BackendDataManager : IBackendDataManager
    {
        public void UpdateData(Dictionary<ProviderType, List<ApartmentData>> data)
        {
            var jsonToWrite = JsonConvert.SerializeObject(data);
            File.WriteAllText("data.json", jsonToWrite);
        }
        public Dictionary<ProviderType, List<object>> GetJobDefinitions()
        {
            var jobDefinitions = new Dictionary<ProviderType, List<object>>();
            jobDefinitions.Add(ProviderType.Avito, new List<object>() { 
                new AvitoApartmentDefinition(0, "https://www.avito.ru/kaliningrad/kvartiry/1-k._kvartira_41m_210et._3471260202") 
            });
            jobDefinitions.Add(ProviderType.Cian, new List<object>() {
                new CianApartmentDefinition(1, "https://kaliningrad.cian.ru/sale/flat/282498775")
            });
            return jobDefinitions;
        }
    }
}
