using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizen
{
    public class CitizenStats
    {
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
            derived_base["mntl"] = (primary["smt"] + primary["wis"])/2;
            derived["mntl"] = derived_base["mntl"];
            derived_base["socl"] = (primary["cha"] + primary["ldr"])/2;
            derived["socl"] = derived_base["socl"];
        }
        // lists of strings that holds the names of all the primary and derived stats
        public List<string> primaryStats = new ()
        {
            "str","dex","smt","wis","cha","ldr"
        };
        public List<string> derivedStats = new List<string>()
        {
            "phys","mntl","socl"
        };

        //dictionaries that hold the values of all the stats, both the base and final
        public Dictionary<string, int> primary_base = new Dictionary<string, int>();
        public Dictionary<string, int> derived_base = new Dictionary<string, int>();
        public Dictionary<string, int> primary = new Dictionary<string, int>();
        public Dictionary<string, int> derived = new Dictionary<string, int>();
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
            //Id = name + "-" + source + "-" + random.Next(10000 - 99999);
        }
        string Name;
        string Description;
        string Source;
        string ModifiedStat;
        int Value;
        bool Temporary;
        int Duration;
        //string Id;

        public string CitizenStatModifierDescription()
        {
            string returnDescription = $"Citizen Stat Modifier: {Name} \nSecond Line??";
            return returnDescription;
        }
    }
}
