using ApartmentPriceParser.Common.Models;
using ApartmentPriceParser.Common.Models.Providers;
using Microsoft.EntityFrameworkCore;

namespace ApartmentPriceParser.Backend.Database
{
    public class BackendDatabaseContext : DbContext
    {
        public DbSet<AvitoApartmentDefinition> AvitoApartmentDefinitions { get; set; }
        public DbSet<CianApartmentDefinition> CianApartmentDefinitions { get; set; }
        public DbSet<ApartmentData> ApartmentDatas { get; set; }
        public BackendDatabaseContext(DbContextOptions<BackendDatabaseContext> options) : base(options) 
        {

        }
    }
}
