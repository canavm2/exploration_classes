using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public partial class Citizen
    {
        public void RefreshDerived()
        {
            DerivedStats["phys"].Full = (PrimaryStats["str"].Full + PrimaryStats["dex"].Full) / 2;
            DerivedStats["mntl"].Full = (PrimaryStats["int"].Full + PrimaryStats["wis"].Full) / 2;
            DerivedStats["socl"].Full = (PrimaryStats["cha"].Full + PrimaryStats["ldr"].Full) / 2;
            DerivedStats["phys"].Unmodified = (PrimaryStats["str"].Unmodified + PrimaryStats["dex"].Unmodified) / 2;
            DerivedStats["mntl"].Unmodified = (PrimaryStats["int"].Unmodified + PrimaryStats["wis"].Unmodified) / 2;
            DerivedStats["socl"].Unmodified = (PrimaryStats["cha"].Unmodified + PrimaryStats["ldr"].Unmodified) / 2;
        }

        public string DescribeCitizen()
        {
            string returnDescription =
                $"\n{Name}, a {Age} year old {Gender}.\n" +
                $"\nTheir stats are:\n\n" +
                DescribeStats() +
                $"\nTheir skills are:\n\n" +
                Skills.Describe() +
                $"\nThis citizen's ID: {Id}\n\n";

            return returnDescription;
        }

        public string DescribeStats()
        {
            //Iterates over all the Primary stats, and provides a string that describes it
            string primaryDesc = "";
            foreach (KeyValuePair<string, Stat> stat in PrimaryStats)
            {
                string tempDesc = $"{stat.Key.ToUpper()}: {stat.Value.Full.ToString()}\n";
                primaryDesc += tempDesc;
            }
            string derivedDesc = "";
            foreach (KeyValuePair<string, Stat> stat in DerivedStats)
            {
                string tempDesc = $"{stat.Key.ToUpper()}: {stat.Value.Full.ToString()}\n";
                derivedDesc += tempDesc;
            }
            string description =
                $"Primary Stats:\n" +
                primaryDesc +
                $"\nDerived Stats:\n" +
                derivedDesc;
            ;
            return description;
        }

        public void AddModifier(Modifier modifier)
        {
            if (!Modifiers.Where(m => m.Name == modifier.Name).Any())
            {
                Modifiers.Add(modifier);
                ApplyModifier(modifier);
            }
            //TODO Determine what happens if the modifier is a duplicate name
        }
        public void RemoveModifier(string name)
        {
            for (int i = 0; i < Modifiers.Count; i++)
            {
                if (Modifiers[i].Name == name)
                {
                    Modifier toremove = Modifiers[i];
                    ApplyModifier(toremove, "remove");
                    Modifiers.RemoveAt(i);
                    break;
                }
            }
        }

        public void ApplyModifier(Modifier modifier, string action = "add")
        {
            int Value = modifier.Value;
            if (action == "remove") Value = -modifier.Value;
            if (modifier.Type == "skill")
            {
                if (Skills.VocSkill.ContainsKey(modifier.ModifiedValue))
                    Skills.VocSkill[modifier.ModifiedValue].Full += Value;
                else if (Skills.ExpSkill.ContainsKey(modifier.ModifiedValue))
                    Skills.ExpSkill[modifier.ModifiedValue].Full += Value;
                else throw new Exception($"Skill Modifier ModifiedValue not found: {modifier.ModifiedValue}");
            }
            else if (modifier.Type == "stat")
            {
                if (PrimaryStats.ContainsKey(modifier.ModifiedValue))
                    PrimaryStats[modifier.ModifiedValue].Full += Value;
                else if (DerivedStats.ContainsKey(modifier.ModifiedValue))
                    DerivedStats[modifier.ModifiedValue].Full += Value;
                else throw new Exception($"Stat Modifier ModifiedValue not found: {modifier.ModifiedValue}");
            }
            else if (modifier.Type == "attribute")
            {
                if (Attributes.ContainsKey(modifier.ModifiedValue))
                    Attributes[modifier.ModifiedValue].Full += Value;
                else throw new Exception($"Attribute Modifier ModifiedValue not found: {modifier.ModifiedValue}");
            }
            else throw new Exception($"Modifier Type not found: {modifier.Type}");
        }

        
    }
}
