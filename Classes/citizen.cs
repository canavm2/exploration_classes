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
        // create citizen class which is the lowest level of a company member or NPC.  
        // constructor requires a name, gender, indexer, and optional age
        // random names can be passed 
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
        public Citizen(string name, string gender, int id, int age, Stats stats)
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Stats = stats;

        }


        #endregion

        #region Descriptors and Stats
        public readonly int Id;
        public string Name;
        public int Age;
        public readonly string Gender;
        public Stats Stats = new();
        #endregion

        #region Methods
        public string Describe()
        {
            string returnDescription =$"{Name}, a {Age} year old {Gender}.\n\n" +
                $"Their primary stats are:\n" +
                $"Strength: {Stats.Primary["str"]}\n" +
                $"Dexterity: {Stats.Primary["dex"]}\n" +
                $"Intelligence: {Stats.Primary["int"]}\n" +
                $"Wisdom: {Stats.Primary["wis"]}\n" +
                $"Charisma: {Stats.Primary["cha"]}\n" +
                $"Leadership: {Stats.Primary["ldr"]}\n\n" +
                $"Their derived stats are:\n" +
                $"Physical: {Stats.Derived["phys"]}\n" +
                $"Mental: {Stats.Derived["mntl"]}\n" +
                $"Social: {Stats.Derived["socl"]}\n\n" +
                $"This citizen's ID: {Id}";

            return returnDescription;
        }
        public int GetId()
        {
            return Id;
        }
        #endregion
    }
}
