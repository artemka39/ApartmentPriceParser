namespace ApartmentPriceParser.Backend.Database
{
    public interface IApartmentDefifnitionRepository
    {
        Task<List<object>> GetAll();
    }
}
