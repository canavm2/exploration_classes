using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTools;
using Newtonsoft.Json;

namespace People
{
    public class Citizen
    {
        #region Constructors
        public Citizen(string name, string gender, IndexId indexer, int age = 0)
        {
            Random random = new();
            if (age == 0)
                age = random.Next(15,40);
            Name = name;
            Age = age;
            List<string> genders = new() { "male", "female", "non-binary" };
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";
            Id = indexer.GetIndex();
        }

        [JsonConstructor]
        public Citizen(string name, string gender, int id, int age, Stats stats, Skills skills)
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Stats = stats;
            Skills = skills;
        }


        #endregion

        #region Descriptors and Stats
        public readonly int Id;
        public string Name;
        public int Age;
        public readonly string Gender;
        public Stats Stats = new();
        public Skills Skills = new();
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
                $"\nThis citizen's ID: {Id}";

            return returnDescription;
        }
        #endregion
    }
}
