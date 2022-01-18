using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using FileTools;
using Newtonsoft.Json;

namespace Company
{
    internal class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, IndexId index, Citizen master, List<Citizen> advisors)
        {
            if (advisors.Count > 7)
                throw new ArgumentException($"There are {advisors.Count} advisors in the list, there must be 7.");
            Name = name;
            CompanyId = index.GetIndex();
            Advisors["master"] = master;
            for (int i = 0; i < 5; i++)
            {
                string AdvisorNumber = "advisor" + i.ToString();
                Advisors[AdvisorNumber] = advisors[i];
            }
            for (int i = 5; i <= 7; i++)
            {
                string BenchNumber = "bench" + i.ToString();
                Advisors[BenchNumber] = advisors[i];
            }

        }

        [JsonConstructor]
        public PlayerCompany(string name, int companyid, Dictionary<string, Citizen> advisors)
        {
            Name = name;
            CompanyId = companyid;
            Advisors = advisors;
        }


        #endregion

        #region Company Descriptors
        public string Name { get; set; }
        public readonly int CompanyId;
        public Dictionary<string, Citizen> Advisors = new();

        #endregion

        #region Methods


        #endregion
    }
}
