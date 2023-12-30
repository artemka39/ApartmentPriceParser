using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceParser.Backend.Database.Repositories
{
    public class CianApartmentDefinitionRepository : IApartmentDefifnitionRepository
    {
        private readonly BackendDatabaseContext dbContext;
        public CianApartmentDefinitionRepository(BackendDatabaseContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<List<object>> GetAll()
        {
            return await dbContext.CianApartmentDefinitions.Select(x => (object)x).ToListAsync();
        }
    }
}
