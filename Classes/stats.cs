using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace People
{
    public class CitizenStats
    {
        #region Constructor
        // an object to hold the primary and derived stats for citizens
        // each object holds the base stats, any modifiers and calculated stats.
        public CitizenStats()
        {
            Random random = new();

            // generates a random value for all primary stats and then calculates the derived stats
            // saves values to both base and "final" stats
            foreach (string pstat in primaryStats)
            {
                primary_base[pstat] = random.Next(10,30);
                primary[pstat] = primary_base[pstat];
            }
            derived_base["phys"] = (primary["str"] + primary["dex"])/2;
            derived["phys"] = derived_base["phys"];
            derived_base["mntl"] = (primary["int"] + primary["wis"])/2;
            derived["mntl"] = derived_base["mntl"];
            derived_base["socl"] = (primary["cha"] + primary["ldr"])/2;
            derived["socl"] = derived_base["socl"];
        }
        #endregion

        #region Dictionaries
        // lists of strings that holds the names of all the primary and derived stats
        public List<string> primaryStats = new ()
        {
            "str","dex","int","wis","cha","ldr"
        };
        public List<string> derivedStats = new List<string>()
        {
            "phys","mntl","socl"
        };

        //dictionaries&lists that hold the various stats and modifiers
        public Dictionary<string, int> primary_base = new();
        public Dictionary<string, int> derived_base = new();
        public Dictionary<string, int> primary = new();
        public Dictionary<string, int> derived = new();
        public List<Modifier> Modifiers = new();
        #endregion

        #region Methods
        // Method that takes all the information required for a modifier, creates a modifier and hten adds it to the list of modifiers.
        // temporary modifiers are stored with a duration.
        public void ApplyModifier(string name, string source, string modstat, int val, bool temp = false, int dur = 0, string desc = "None.")
        {
            Modifier modifier = new(name, source, modstat, val, temp, dur, desc);
            //Checks to see if the modifiers Id already exists
            //if it exists it replaces the current isntance with the new one
            //if not it adds the modifier to the list
            if (Modifiers.Count > 0)
            {
                for (int i = 0; i < Modifiers.Count; i++)
                {
                    if (Modifiers[i].Id == modifier.Id)
                    {
                        Modifiers[i] = modifier;
                    }
                    else Modifiers.Add(modifier);
                }
            }
            else Modifiers.Add(modifier);

            //checks which stat is being modified and changes the "final" dictionary
            if (primaryStats.Contains(modifier.ModifiedStat))
            {
                primary[modifier.ModifiedStat] += modifier.Value;
                Debug.WriteLine($"Modified: {primary[modifier.ModifiedStat]}");
            }
            else if (derivedStats.Contains(modifier.ModifiedStat))
            {
                derived[modifier.ModifiedStat] += modifier.Value;
                Debug.WriteLine($"Modified: {derived[modifier.ModifiedStat]}");
            }
            //TODO Change exception
            else throw new Exception($"Error: Stat not found: {modifier.ModifiedStat}");
        }

        public void RemoveModifier(string id)
        {
            //Modifier modifier = Modifiers[id];
        }
        #endregion

        #region subclasses
        // a class used to apply modifiers to the stats, should only be instatiated with ApplyModifier above.
        public class Modifier
        {
            public Modifier(string name, string source, string modstat, int val, bool temp, int dur, string desc)
            {
                Name = name;
                Description = desc;
                Source = source;
                ModifiedStat = modstat;
                Value = val;
                Temporary = temp;
                Duration = dur;
                //TODO change exception
                if (dur < 0) throw new Exception($"Negative Duration: {dur}");
                Id = source + "-" + name;
            }
            public readonly string Name;
            public readonly string Description;
            public readonly string Source;
            public readonly string ModifiedStat;
            public readonly int Value;
            public readonly bool Temporary;
            public readonly int Duration;
            public readonly string Id;

            public string Summary()
            {
                string returnSummary = $"Citizen Stat Modifier: {Name}\n" +
                    $"{ModifiedStat}: {Value}\n" +
                    $"Description: {Description}";
                if (Temporary)
                    returnSummary = returnSummary + $"\n" +
                        $"Duration: {Duration}\n";
                return returnSummary;
            }
        }
        #endregion
    }


}
