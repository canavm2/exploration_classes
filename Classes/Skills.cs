using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    internal class Skills
    {        public Skills()
        {
            Random random = new();
            //These stat lists are repeated in ApplyModifier, so that they aren't stored in the Stats Object.
            List<string> vocSkillsList = new() { "str", "dex", "int", "wis", "cha", "ldr" };
            List<string> expSkillsList = new() { "phys", "mntl", "socl" };

            // generates a random value for all primary stats and then calculates the derived stats
            // saves values to both base and "final" stats
            foreach (string skill in vocSkillsList)
            {
                vocSkill_base[skill] = random.Next(10, 30);
                vocSkill[skill] = vocSkill_base[skill];
            }
            foreach (string skill in expSkillsList)
            {
                expSkill_base[skill] = random.Next(10, 30);
                expSkill[skill] = expSkill_base[skill];
            }
        }

        public Dictionary<string, int> vocSkill_base = new();
        public Dictionary<string, int> expSkill_base = new();
        public Dictionary<string, int> vocSkill = new();
        public Dictionary<string, int> expSkill = new();



        #region subclasses
        // a class used to apply modifiers to the stats, should only be instatiated with ApplyModifier above.
        public class Modifier
        {
            public Modifier(string name, string source, string modskill, int val, bool temp, int dur, string desc)
            {
                Name = name;
                Description = desc;
                Source = source;
                ModifiedSkill = modskill;
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
            public readonly string ModifiedSkill;
            public readonly int Value;
            public readonly bool Temporary;
            public readonly int Duration;
            public readonly string Id;

            public string Summary()
            {
                string returnSummary = $"Citizen Skill Modifier: {Name}\n" +
                    $"{ModifiedSkill}: {Value}\n" +
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
