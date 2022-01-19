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
    public class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, IndexId index, Citizen master, List<Citizen> advisors)
        {
            //advisors must have 7 citizens in it, less than 7 will cause and error, more than 7 will ignore the rest
            if (advisors.Count != 7)
                throw new ArgumentException($"There are {advisors.Count} advisors in the list, there must be 7.");
            Name = name;
            CompanyId = index.GetIndex();
            Advisors["master"] = master;
            //Sets the first 5 citizens in advisors to the other advisors
            for (int i = 0; i < 5; i++)
            {
                string AdvisorNumber = "advisor" + (i+1).ToString();
                Advisors[AdvisorNumber] = advisors[i];
            }
            //Sets the last 2 advisors to bench positions
            for (int i = 5; i < 7; i++)
            {
                string BenchNumber = "bench" + (i-4).ToString();
                Advisors[BenchNumber] = advisors[i];
            }
            //Creates relationship between the members
            UpdateSocial();

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
        public string Describe()
        {
            string advisorDescription = "";
            foreach (KeyValuePair<string, Citizen> advisor in Advisors)
            {
                if (advisor.Key.Contains("advisor"))
                    advisorDescription += $"{advisor.Value.Name}\n";
            }
            string benchDescription = "";
            foreach (KeyValuePair<string, Citizen> advisor in Advisors)
            {
                if (advisor.Key.Contains("bench"))
                    benchDescription += $"{advisor.Value.Name}\n";
            }
            string companyDescription =
                $"The company's name is: {Name}.\n" +
                $"ID: {CompanyId}\n\n" +
                $"The company master is {Advisors["master"].Name}.\n\n" +
                $"The company advisors are:\n" +
                advisorDescription +
                $"These advisors are on the bench:\n" +
                benchDescription;

            return companyDescription;
        }

        public void UpdateSocial()
        {
            int count = 0;
            //iterates through each citizen in the list of Advisors
            foreach (Citizen advisor in Advisors.Values)
            {
                //for each Advisor, determines if there is a relationship with all the other advisors
                foreach (Citizen otheradvisor in Advisors.Values)
                {
                    //ignores itself during the search
                    if (otheradvisor.Id != advisor.Id)
                    {
                        count++;
                        //Checks to see if the relationship already exists, if not it creates a new relationship with the otheradvisor
                        if (!advisor.Social.Relationships.ContainsKey(otheradvisor.Id))
                        {
                            advisor.Social.CreateNewRelationship(otheradvisor);
                        }
                    }
                }
            }
            Console.WriteLine($"Checked {count} advisor relationships.");
        }
        
        #endregion
    }
}
