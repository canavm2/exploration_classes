using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTools;
using Newtonsoft.Json;

namespace People
{
    public class CitizenCache
    {
        public CitizenCache(IndexId index, int size = 0)
        {
            NameList nameList = new();
            FemaleCitizens = new();
            MaleCitizens = new();
            NBCitizens = new();
            for (int i = 0; i < size; i++)
            {
                Citizen femaleCitizen = new(nameList.generateName("female"), "female", index);
                FemaleCitizens.Add(femaleCitizen);
                Citizen maleCitizen = new(nameList.generateName("male"), "male", index);
                MaleCitizens.Add(maleCitizen);
                Citizen nbCitizen = new(nameList.generateName("non-binary"), "non-binary", index);
                NBCitizens.Add(nbCitizen);
            }
        }
        [JsonConstructor]
        public CitizenCache(List<Citizen> femalecitizens, List<Citizen> malecitizens, List<Citizen> nbcitizens)
        {
            FemaleCitizens = femalecitizens;
            MaleCitizens = malecitizens;
            NBCitizens = nbcitizens;
        }
        public List<Citizen> FemaleCitizens { get; }
        public List<Citizen> MaleCitizens { get; }
        public List<Citizen> NBCitizens { get; }

        public void CacheCitizen(Citizen citizen)
        {
            if (citizen.Gender == "female") FemaleCitizens.Add(citizen);
            else if (citizen.Gender == "male") MaleCitizens.Add(citizen);
            else NBCitizens.Add(citizen);
        }
        public Citizen GetRandomCitizen(string gender = "random")
        {
            Citizen returncitizen;
            Random random = new Random();
            int index;
            if (gender == "random")
            {
                string[] genders = new string[] { "female", "male", "non-binary" };
                index = random.Next(genders.Length);
                gender = genders[index];
            }
            if (gender == "female")
            {
                index = random.Next(FemaleCitizens.Count);
                returncitizen = FemaleCitizens[index];
                FemaleCitizens.RemoveAt(index);
            }
            else if (gender == "male")
            {
                index = random.Next(MaleCitizens.Count);
                returncitizen = MaleCitizens[index];
                MaleCitizens.RemoveAt(index);
            }
            else
            {
                index = random.Next(NBCitizens.Count);
                returncitizen = NBCitizens[index];
                NBCitizens.RemoveAt(index);
            }
            return returncitizen;
        }
    }
    public class Citizen
    {
        #region Constructors
        public Citizen(string name, string gender, IndexId indexer, int age = 0)
        {
            Random random = new();
            if (age == 0)
                age = random.Next(15, 40);
            Name = name;
            Age = age;
            List<string> genders = new() { "male", "female", "non-binary" };
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";
            Id = indexer.GetIndex();
            Stats = new();
            Skills = new();
            Attributes = new();
        }

        [JsonConstructor]
        public Citizen(string name, string gender, int id, int age, Stats stats, Skills skills, Attributes attributes)
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Stats = stats;
            Skills = skills;
            Attributes = attributes;
        }
        #endregion

        #region Descriptors and Stats
        public readonly int Id;
        public readonly string Name;
        public int Age;
        public readonly string Gender;
        public Stats Stats;
        public Skills Skills;
        public Attributes Attributes;
        public Traits Traits;
        #endregion

        #region Methods
        public string Describe()
        {
            string returnDescription =
                $"\n{Name}, a {Age} year old {Gender}.\n" +
                $"\nTheir stats are:\n\n" +
                Stats.Describe() +
                $"\nTheir skills are:\n\n" +
                Skills.Describe() +
                $"\nThis citizen's ID: {Id}\n\n";

            return returnDescription;
        }
        #endregion
    }
}
