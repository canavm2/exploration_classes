using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using FileTools;
using Newtonsoft.Json;
using Relationships;

namespace Company
{
    public class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, IndexId index, Citizen master, List<Citizen> advisors)
        {
            Social = new();
            if (advisors.Count != 7)
                throw new ArgumentException($"There are {advisors.Count} advisors in the list, there must be 7.");
            Name = name;
            CompanyId = index.GetIndex();
            AddAdvisor(master, "master");
            //Sets the first 5 citizens in advisors to the other advisors
            for (int i = 0; i < 5; i++)
            {
                string advisorNumber = "advisor" + (i+1).ToString();
                AddAdvisor(advisors[i], advisorNumber);
            }
            //Sets the last 2 advisors to bench positions
            for (int i = 5; i < 7; i++)
            {
                string benchNumber = "bench" + (i-4).ToString();
                AddAdvisor(advisors[i], benchNumber);
            }
        }

        [JsonConstructor]
        public PlayerCompany(string name, int companyid, Dictionary<string, Citizen> advisors, Social social)
        {
            Name = name;
            CompanyId = companyid;
            Advisors = advisors;
            Social = social;
        }


        #endregion

        #region Company Descriptors
        public string Name { get; set; }
        public readonly int CompanyId;
        public Dictionary<string, Citizen> Advisors = new();
        public Social Social;

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

        internal void AddAdvisor(Citizen citizen, string role)
        {
            //TODO verify the role is acceptible
            Advisors[role] = citizen;
            foreach (Citizen advisor in Advisors.Values)
            {
                if (advisor.Id != citizen.Id)
                {
                    Relationship relationship = new Relationship(citizen, advisor);
                    Social.Relationships[relationship.Id] = relationship;
                }
            }
        }

        public List<Relationship> UpdateSocial()
        {
            List<int> advisorIds = new();
            List<Relationship> oldRelationships = new();
            int relationshipCount = 0;
            foreach (Citizen advisor in Advisors.Values)
            {
                advisorIds.Add(advisor.Id);
            }
            //iterates through each relationship in Social
            foreach (KeyValuePair<string, Relationship> kvp in Social.Relationships)
            {
                string key = kvp.Key;
                string[] ids = key.Split("-");
                int id1 = int.Parse(ids[0]);
                int id2 = int.Parse(ids[1]);
                //check to see if the key contains ids from two current advisors
                //if it doesnt match 2 current advisors, it removes it and returns it
                if (advisorIds.Contains(id1) && advisorIds.Contains(id2))
                {
                    relationshipCount++;
                }
                else
                {
                    oldRelationships.Add(kvp.Value);
                    Social.Relationships.Remove(kvp.Key);
                }
            }
            Console.WriteLine($"There are {relationshipCount} good relationships, and {oldRelationships.Count} old relationships removed.");
            return oldRelationships;
        }

        #endregion
    }
}
