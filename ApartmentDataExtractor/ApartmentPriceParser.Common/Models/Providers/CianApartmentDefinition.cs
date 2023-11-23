using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApartmentPriceParser.Common.Models.Providers
{
    public class CianApartmentDefinition
    {
        public long Id { get; set; }
        public string CianId { get; set; }
        public CianApartmentDefinition(long id, string url)
        {
            Id = id;
            var parts = url.Split('/');
            CianId = parts[5];
        }
    }
}
