using Refit;

namespace Client.DataProviders.Clients;

public interface ICianClient
{
    [Get("/sale/flat/{id}")]
    Task<string> GetApartmentData(string id);
}
