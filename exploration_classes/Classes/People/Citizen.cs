using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTools;
using Newtonsoft.Json;

namespace People
{
    public partial class Citizen
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
            Skills = new();
            Modifiers = new();
            Traits = new();

            #region ConstructStats
            ListTool listTool = new ListTool();
            PrimaryStats = new();
            DerivedStats = new();
            Modifiers = new();

            foreach (string pstat in listTool.PrimaryStats)
            {
                PrimaryStats[pstat] = new(random.Next(10, 30));
            }
            foreach (string dstat in listTool.DerivedStats)
            {
                DerivedStats[dstat] = new(0);
            }
            RefreshDerived();
            #endregion

            #region ConstructAttributes
            Attributes = new();
            foreach (string attribute in listTool.Attributes)
                Attributes.Add(attribute, new Attribute());
            #endregion

            #region ConstructTraits
            //TODO Actually Construct Traits
            Traits = new();
            #endregion
        }

        [JsonConstructor]
        public Citizen(
            string name,
            string gender,
            int id, int age,
            Skills skills,
            Dictionary<string, Stat> primarystats,
            Dictionary<string, Stat> derivedstats,
            Dictionary<string, Modifier> modifiers,
            Dictionary<string, Attribute> attributes,
            Dictionary<string, Trait> traits
            )
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Skills = skills;
            PrimaryStats = primarystats;
            DerivedStats = derivedstats;
            Modifiers = modifiers;
            Attributes = attributes;
            Traits = traits;
        }
        #endregion

        #region Dictionaries and Properties
        public readonly int Id;
        public readonly string Name;
        public int Age;
        public readonly string Gender;
        public Skills Skills;
        public Dictionary<string, Stat> PrimaryStats;
        public Dictionary<string, Stat> DerivedStats;
        public Dictionary<string, Attribute> Attributes;
        public Dictionary<string,Modifier> Modifiers { get; }
        public Dictionary<string,Trait> Traits { get; }
        #endregion

        #region Subclasses
        public class Attribute
        {
            #region Constructors
            public Attribute()
            {
                Full = 10;
                Unmodified = 10;
            }

            [JsonConstructor]
            public Attribute(int full, int unmodified)
            {
                Full = full;
                Unmodified = unmodified;
            }
            #endregion
            public int Full;
            public int Unmodified;
        }
        public class Stat
        {
            public Stat(int unmod)
            {
                Unmodified = unmod;
                Full = unmod;
            }

            [JsonConstructor]
            public Stat(int unmodified, int full)
            {
                Unmodified = unmodified;
                Full = full;
            }
            public int Unmodified;
            public int Full;
        }
        public class Trait
        {
            public Trait(string name, int tier, List<Modifier> modifiers)
            {
                Name = name;
                Tier = tier;
                Modifiers = modifiers;
                Known = false;
            }
            [JsonConstructor]
            public Trait(string name, int tier, List<Modifier> modifiers, Boolean known)
            {
                Name = name;
                Tier = tier;
                Modifiers = modifiers;
                Known = known;
            }

            public readonly string Name;
            public readonly int Tier;
            public Boolean Known;
            public readonly List<Modifier> Modifiers;

            public string Summary()
            {
                string returnstring = $"\nTrait: {Name}." +
                    $"\nModifiers:\n";
                foreach (Modifier modifier in Modifiers)
                    returnstring += modifier.Summary();
                return returnstring;
            }
        }
        #endregion
    }
}
