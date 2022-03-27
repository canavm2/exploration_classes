using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace Company
{
    public class CompanyCache
    {
        #region Constructor
        public CompanyCache()
        {
            id = Guid.NewGuid();
            PlayerCompanies = new();
        }

        [JsonConstructor]
        public CompanyCache(Guid ID, Dictionary<Guid, PlayerCompany> playerCompanies)
        {
            id=ID;
            PlayerCompanies = playerCompanies;
        }

        public Guid id { get; set; }
        public Dictionary<Guid, PlayerCompany> PlayerCompanies { get; set; }

        #endregion

    }
}
