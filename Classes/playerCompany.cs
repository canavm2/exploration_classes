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

        //Method to add a citizen to a company when the company is empty, In order to add a citizen into an occupied role, use ReplaceAdvisor
        public void AddAdvisor(Citizen citizen, string role)
        {
            Advisors[role] = citizen;
            foreach (Citizen advisor in Advisors.Values)
            {
                if (advisor.Id != citizen.Id)
                {
                    Social.Relationship relationship = new Social.Relationship(citizen, advisor);
                    Social.Relationships[relationship.Id] = relationship;
                }
            }
        }


        //Replaces the citizen provided with teh citizen currently in the provided role.  The replaced citizen is returned, so it can be stored in the citizen vault.
        public Citizen ReplaceAdvisor(Citizen citizen, string role)
        {
            if (Advisors[role] == null) throw new ArgumentNullException($"No citizen to replace in role: {role}");
            Citizen replacedCitizen = Advisors[role];
            Advisors[role] = citizen;
            //TODO deal with relationships
            return replacedCitizen;
        }
        public List<Social.Relationship> UpdateSocial()
        {
            List<int> advisorIds = new();
            List<Social.Relationship> oldRelationships = new();
            foreach (Citizen advisor in Advisors.Values)
            {
                advisorIds.Add(advisor.Id);
            }
            //iterates through each relationship in Social
            foreach (KeyValuePair<string, Social.Relationship> kvp in Social.Relationships)
            {
                string key = kvp.Key;
                string[] ids = key.Split("-");
                int id1 = int.Parse(ids[0]);
                int id2 = int.Parse(ids[1]);

            }
            Console.WriteLine($"");
            return oldRelationships;
        }

        #endregion
    }
}
