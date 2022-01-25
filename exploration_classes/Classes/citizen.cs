﻿using System;
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

            #region ConstructStats
            List<string> primaryStats = new() { "str", "dex", "int", "wis", "cha", "ldr" };
            List<string> derivedStats = new() { "phys", "mntl", "socl" };
            PrimaryStats = new();
            DerivedStats = new();
            StatModifiers = new();

            foreach (string pstat in primaryStats)
            {
                PrimaryStats[pstat] = new(random.Next(10, 30));
            }
            DerivedStats["phys"] = new((PrimaryStats["str"].Unmodified + PrimaryStats["dex"].Unmodified) / 2);
            DerivedStats["mntl"] = new((PrimaryStats["int"].Unmodified + PrimaryStats["wis"].Unmodified) / 2);
            DerivedStats["socl"] = new((PrimaryStats["cha"].Unmodified + PrimaryStats["ldr"].Unmodified) / 2);
            #endregion

            #region ConstructAttributes
            List<string> attributes = new List<string>() {
                "Health",
                "Happiness",
                "Motivation",
                "Psyche"
            };
            Attributes = new();
            AttributeModifiers = new();
            foreach (string attribute in attributes)
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
            List<Modifier> statmodifiers,
            Dictionary<string, Attribute> attributes,
            List<Modifier> attributemodifiers,
            List<Trait> traits
            )
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Skills = skills;
            PrimaryStats = primarystats;
            DerivedStats = derivedstats;
            StatModifiers = statmodifiers;
            Attributes = attributes;
            AttributeModifiers = attributemodifiers;
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
        public List<Modifier> StatModifiers;
        public Dictionary<string, Attribute> Attributes;
        public List<Modifier> AttributeModifiers;
        public List<Trait> Traits;
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
            public Trait(string name, List<Modifier> modifiers)
            {
                Name = name;
                Modifiers = modifiers;
            }
            public readonly string Name;
            public readonly List<Modifier> Modifiers;
        }
        #endregion
    }
}
