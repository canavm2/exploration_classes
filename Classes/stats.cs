using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Citizen
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

        //dictionaries that hold the values of all the stats, both the base and final
        public Dictionary<string, int> primary_base = new();
        public Dictionary<string, int> derived_base = new();
        public Dictionary<string, int> primary = new();
        public Dictionary<string, int> derived = new();

        public Dictionary<string, CitizenStatModifier> Modifiers = new();
        #endregion

        #region Methods
        public void ApplyModifier(CitizenStatModifier modifier)
        {
            Debug.WriteLine("Applying Modifier");
            Debug.WriteLine(modifier.CitizenStatModifierDescription());
            Modifiers[modifier.Id] = modifier;
            Debug.WriteLine($"Modifying: {modifier.ModifiedStat} by {modifier.Value}");
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
            else throw new Exception($"Error: Stat not found: {modifier.ModifiedStat}");
        }

        public void RemoveModifier(string id)
        {
            CitizenStatModifier modifier = Modifiers[id];
        }
        #endregion
    }


    // a class used to apply modifiers to the stats
    public class CitizenStatModifier
    {
        public CitizenStatModifier(string name, string source, string modstat, int val, bool temp = false, int dur = 0, string desc = "None." )
        {
            Random random = new();
            Name = name;
            Description = desc;
            Source = source;
            ModifiedStat = modstat;
            Value = val;
            Temporary = temp;
            Duration = dur;
            if (dur < 0) throw new Exception($"Negative Duration: {dur}");
            if (temp)
                Id = "temp" + "-" + random.Next(10000, 99999);
            else Id = source + "-" + name;
        }
        string Name;
        string Description;
        string Source;
        public readonly string ModifiedStat;
        public int Value;
        bool Temporary;
        int Duration;
        public string Id;

        public string CitizenStatModifierDescription()
        {
            string returnDescription = $"Citizen Stat Modifier: {Name}\n" +
                $"{ModifiedStat}: {Value}\n" +
                $"Description: {Description}";
            if (Temporary)
                returnDescription = returnDescription + $"\n" +
                    $"Duration: {Duration}";
            return returnDescription;
        }
    }
}
