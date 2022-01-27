﻿using System;
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
            DerivedStats["PHYS"].Full = (PrimaryStats["STR"].Full + PrimaryStats["DEX"].Full) / 2;
            DerivedStats["MNTL"].Full = (PrimaryStats["INT"].Full + PrimaryStats["WIS"].Full) / 2;
            DerivedStats["SOCL"].Full = (PrimaryStats["CHA"].Full + PrimaryStats["LDR"].Full) / 2;
            DerivedStats["PHYS"].Unmodified = (PrimaryStats["STR"].Unmodified + PrimaryStats["DEX"].Unmodified) / 2;
            DerivedStats["MNTL"].Unmodified = (PrimaryStats["INT"].Unmodified + PrimaryStats["WIS"].Unmodified) / 2;
            DerivedStats["SOCL"].Unmodified = (PrimaryStats["CHA"].Unmodified + PrimaryStats["LDR"].Unmodified) / 2;
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
            if (Modifiers.ContainsKey(modifier.Name))
            {
                Modifiers[modifier.Name] = modifier;
                ApplyModifier(Modifiers[modifier.Name]);
            }
            //TODO Determine what happens if the modifier is a duplicate name
        }

        public void RemoveModifier(string name)
        {
            if (Modifiers.ContainsKey(name))
            {
                ApplyModifier(Modifiers[name],"remove");
                Modifiers.Remove(name);
            }
            else throw new Exception($"Modifiers key not found: {name}");
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
                {
                    PrimaryStats[modifier.ModifiedValue].Full += Value;
                    RefreshDerived();
                }
                else if (DerivedStats.ContainsKey(modifier.ModifiedValue))
                    throw new Exception($"Derived stats shouldnt have modifiers: {modifier.ModifiedValue}"); //If derived stats had modifiers, then refreshing modifiers would be tough (not impossible)
                //DerivedStats[modifier.ModifiedValue].Full += Value;
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

        public void AddTrait(Trait trait)
        {
            if (!Traits.ContainsKey(trait.Name))
            {
                Traits[trait.Name] = trait;
                foreach (Modifier modifier in Traits[trait.Name].Modifiers)
                {
                    ApplyModifier(modifier);
                }
            }
        }

        public void RemoveTrait(string name)
        {
            if (Traits.ContainsKey(name))
            {
                foreach (Modifier modifier in Traits[name].Modifiers)
                {
                    ApplyModifier(modifier, "remove");
                }
                Traits.Remove(name);
            }
        }
    }
}
