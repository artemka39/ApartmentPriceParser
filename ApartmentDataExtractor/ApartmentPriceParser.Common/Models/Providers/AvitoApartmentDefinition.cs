using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentPriceParser.Common.Models.Providers
{
    public class AvitoApartmentDefinition
    {
        public long Id { get; set; }
        public string City { get; set; }
        public string AvitoId { get; set; }
        public AvitoApartmentDefinition(long id, string url)
        {
            Id = id;
            var parts = url.Split('/');
            City = parts[3];
            AvitoId = parts[5];
        }
    }
}
