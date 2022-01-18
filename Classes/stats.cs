using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json;

namespace People
{
    public class Stats
    {
        #region Constructor
        // an object to hold the primary and derived stats for citizens
        // each object holds the base stats, any modifiers and calculated stats.
        public Stats()
        {
            Random random = new();
            //These stat lists are repeated in ApplyModifier, so that they aren't stored in the Stats Object.
            List<string> primaryStats = new() {"str","dex","int","wis","cha","ldr"};
            List<string> derivedStats = new() {"phys", "mntl", "socl"};
        
            // generates a random value for all primary stats and then calculates the derived stats
            // saves values to both base and "final" stats
            foreach (string pstat in primaryStats)
            {
                PrimaryBase[pstat] = random.Next(10, 30);
                Primary[pstat] = PrimaryBase[pstat];
            }
            DerivedBase["phys"] = (Primary["str"] + Primary["dex"]) / 2;
            Derived["phys"] = DerivedBase["phys"];
            DerivedBase["mntl"] = (Primary["int"] + Primary["wis"]) / 2;
            Derived["mntl"] = DerivedBase["mntl"];
            DerivedBase["socl"] = (Primary["cha"] + Primary["ldr"]) / 2;
            Derived["socl"] = DerivedBase["socl"];
        }

        [JsonConstructor]
        //Json Deserialization uses the name of the property as the parameter so it is ideal if they match, as done below
        public Stats(List<Modifier> modifiers, Dictionary<string, int> primarybase, Dictionary<string, int> derivedbase, Dictionary<string, int> primary, Dictionary<string, int> derived)
        {
            Modifiers = modifiers;
            PrimaryBase = primarybase;
            DerivedBase = derivedbase;
            Primary = primary;
            Derived = derived;
        }
        #endregion

        #region Dictionaries
        // lists of strings that holds the names of all the primary and derived stats



        //dictionaries&lists that hold the various stats and modifiers
        public Dictionary<string, int> PrimaryBase = new();
        public Dictionary<string, int> DerivedBase = new();
        public Dictionary<string, int> Primary = new();
        public Dictionary<string, int> Derived = new();
        public List<Modifier> Modifiers = new();
        #endregion

        #region Methods
        // Method that takes all the information required for a modifier, creates a modifier, adds it to the list of modifiers, and then refreshes modifiers.
        // temporary modifiers are stored with a duration.
        public void ApplyModifier(string name, string source, string modstat, int val, bool temp = false, int dur = 0, string desc = "None.")
        {
            Modifier modifier = new(name, source, modstat, val, temp, dur, desc);

            //Checks to see if the modifiers Id already exists
            //if it exists it replaces the current instance with the new one
            //if not it adds the modifier to the list
            if (Modifiers.Count > 0)
            {
                for (int i = 0; i < Modifiers.Count; i++)
                {
                    if (Modifiers[i].Id == modifier.Id)
                    {
                        Modifiers[i] = modifier;
                        break;
                    }
                    else Modifiers.Add(modifier);
                }
            }
            else Modifiers.Add(modifier);
            RefreshModifiers();
        }

        //Method that searches for a modifier by Id, removes that modifier, and then refreshes the modifiers.
        public void RemoveModifier(string id)
        {
            //These stat lists are repeated in CitizenStats, so that they aren't stored in the Stats Object.
            List<string> primaryStats = new() { "str", "dex", "int", "wis", "cha", "ldr" };
            List<string> derivedStats = new() { "phys", "mntl", "socl" };

            //iterates through the modifier list looking for the Id, removing it when it finds it.
            for (int i = 0; i < Modifiers.Count; i++)
            {
                if (Modifiers[i].Id == id)
                {
                    Modifiers.RemoveAt(i);
                    break;
                }
                //TODO Change exception
                else throw new Exception($"Error: Modifier not found: {id}");
            }
            RefreshModifiers();
        }

        //Method used to refresh the stat dictionaries, reset the "final" stats with the base stats
        //and then interates through the modifier list reapplying them all.
        public void RefreshModifiers()
        {
            List<string> primaryStats = new() { "str", "dex", "int", "wis", "cha", "ldr" };
            List<string> derivedStats = new() { "phys", "mntl", "socl" };

            //Resets the "final" stats to the base values
            foreach (String stat in primaryStats)
                Primary[stat] = PrimaryBase[stat];
            foreach (String stat in derivedStats)
                Derived[stat] = DerivedBase[stat];

            //iterates through the modifiers and reapplys them
            foreach (Modifier modifier in Modifiers)
            {
                if (primaryStats.Contains(modifier.ModifiedStat))
                {
                    Primary[modifier.ModifiedStat] += modifier.Value;
                    Debug.WriteLine($"Modified: {Primary[modifier.ModifiedStat]}");
                }
                else if (derivedStats.Contains(modifier.ModifiedStat))
                {
                    Derived[modifier.ModifiedStat] += modifier.Value;
                    Debug.WriteLine($"Modified: {Derived[modifier.ModifiedStat]}");
                }
                //TODO Change exception
                else throw new Exception($"Error: Stat not found: {modifier.ModifiedStat}");

            }
            RefreshDerived();
        }

        public void RefreshDerived()
        {
            Derived["phys"] = (Primary["str"] + Primary["dex"]) / 2;
            Derived["mntl"] = (Primary["int"] + Primary["wis"]) / 2;
            Derived["socl"] = (Primary["cha"] + Primary["ldr"]) / 2;
        }
        #endregion

        #region subclasses
        // a class used to apply modifiers to the stats, should only be instatiated with ApplyModifier above.
        public class Modifier
        {
            // IMPORTANT: Json Deserialization uses the name of the property as the parameter
            // if the property is readonly it must match or it will not be able to change it after the constructor
            public Modifier(string name, string source, string modifiedstat, int value, bool temporary, int duration, string description)
            {
                Name = name;
                Description = description;
                Source = source;
                ModifiedStat = modifiedstat;
                Value = value;
                Temporary = temporary;
                Duration = duration;
                //TODO change exception
                if (duration < 0) throw new Exception($"Negative Duration: {duration}");
                Id = name + "-" + source;
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
                    $"Description: {Description}\n" +
                    $"ID: {Id}";
                if (Temporary)
                    returnSummary = returnSummary + $"\n" +
                        $"Duration: {Duration}\n";
                return returnSummary;
            }
        }
        #endregion
    }


}
