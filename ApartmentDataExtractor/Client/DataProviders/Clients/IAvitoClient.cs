using Refit;

namespace Client.DataProviders.Clients;

public interface IAvitoClient
{
    [Get("/{city}/kvartiry/{id}")]
    Task<string> GetApartmentData(string city, string id);
}
