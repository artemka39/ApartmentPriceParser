using ApartmentPriceParser.Common.Models;

namespace Client.DataProviders
{
    public interface IDataProvider
    {
        Task<ApartmentData> GetApartmentData(object request);
    }
}
