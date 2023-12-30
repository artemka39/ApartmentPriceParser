using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceParser.Backend.Database.Repositories
{
    public class AvitoApartmentDefinitionRepository : IApartmentDefifnitionRepository
    {
        private readonly BackendDatabaseContext dbContext;
        public AvitoApartmentDefinitionRepository(BackendDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<object>> GetAll()
        {
            return await dbContext.AvitoApartmentDefinitions.Select(x => (object)x).ToListAsync();
        }
    }
}
