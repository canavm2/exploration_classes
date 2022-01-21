using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using FileTools;
using Newtonsoft.Json;
using Relation;

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
            Skills = new("company");
            UpdateCompanySkills();
        }

        [JsonConstructor]
        public PlayerCompany(string name, int companyid, Dictionary<string, Citizen> advisors, Social social, Skills skills)
        {
            Name = name;
            CompanyId = companyid;
            Advisors = advisors;
            Social = social;
            Skills = skills;
        }


        #endregion

        #region Company Descriptors
        public string Name { get; set; }
        public readonly int CompanyId;
        public Dictionary<string, Citizen> Advisors = new();
        public Social Social;
        public Skills Skills;

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
                $"\nThese advisors are on the bench:\n" +
                benchDescription +
                $"\nThe company skills are:\n" +
                Skills.Describe()               
                ;
            return companyDescription;
        }
        
        //used to ensure all the skills are up to date
        internal void UpdateCompanySkills()
        {
            foreach (KeyValuePair<string,int> kvp in Skills.VocSkill)
            {
                UpdateCompanySkill(kvp.Key, "voc");
            }
            foreach (KeyValuePair<string, int> kvp in Skills.ExpSkill)
            {
                UpdateCompanySkill(kvp.Key, "exp");
            }
        }
        //Used to update a single skill
        internal void UpdateCompanySkill(string skill, string type)
        {
            if (type == "voc")
            {
                List<int> skillvalues = new();
                foreach (Citizen citizen in Advisors.Values)
                {
                    skillvalues.Add(citizen.Skills.VocSkill[skill]);
                }
                skillvalues.Sort();
                skillvalues.Reverse();
                Skills.VocSkill[skill] = (skillvalues[0] + skillvalues[1]) / 2;
            }
            else if (type == "exp")
            {
                List<int> skillvalues = new();
                foreach (Citizen citizen in Advisors.Values)
                {
                    skillvalues.Add(citizen.Skills.ExpSkill[skill]);
                }
                skillvalues.Sort();
                skillvalues.Reverse();
                Skills.ExpSkill[skill] = (skillvalues[0] + skillvalues[1]) / 2;
            }
            else throw new Exception($"type must be voc or exp, not: {type}");
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
        #endregion
    }
}
