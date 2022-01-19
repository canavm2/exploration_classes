﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileTools;
using Newtonsoft.Json;

namespace People
{
    public class CitizenCache
    {
        public CitizenCache()
        {
            FemaleCitizens = new();
            MaleCitizens = new();
            NBCitizens = new();
        }
        [JsonConstructor]
        public CitizenCache(List<Citizen> femalecitizens, List<Citizen> malecitizens, List<Citizen> nbcitizens)
        {
            FemaleCitizens = femalecitizens;
            MaleCitizens = malecitizens;
            NBCitizens = nbcitizens;
        }
        public List<Citizen> FemaleCitizens;
        public List<Citizen> MaleCitizens;
        public List<Citizen> NBCitizens;

        public void CacheCitizen(Citizen citizen)
        {
            if (citizen.Gender == "female") FemaleCitizens.Add(citizen);
            else if (citizen.Gender == "male") MaleCitizens.Add(citizen);
            else NBCitizens.Add(citizen);
        }
        public Citizen GetCitizen(string gender = "random")
        {
            Citizen returncitizen;
            Random random = new Random();
            int index;
            if (gender == "random")
            {
                string[] genders = new string[] { "female", "male", "non-binary" };
                index = random.Next(genders.Length);
                gender = genders[index];
            }
            if (gender == "female")
            {
                index = random.Next(FemaleCitizens.Count);
                returncitizen = FemaleCitizens[index];
                FemaleCitizens.RemoveAt(index);
            }
            else if (gender == "male")
            {
                index = random.Next(MaleCitizens.Count);
                returncitizen = MaleCitizens[index];
                MaleCitizens.RemoveAt(index);
            }
            else
            {
                index = random.Next(NBCitizens.Count);
                returncitizen = NBCitizens[index];
                NBCitizens.RemoveAt(index);
            }
            return returncitizen;
        }
    }
    public class Citizen
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
            Stats = new();
            Skills = new();
        }

        [JsonConstructor]
        public Citizen(string name, string gender, int id, int age, Stats stats, Skills skills)
        {
            Name = name;
            Gender = gender;
            Id = id;
            Age = age;
            Stats = stats;
            Skills = skills;
        }


        #endregion

        #region Descriptors and Stats
        public readonly int Id;
        public readonly string Name;
        public int Age;
        public readonly string Gender;
        public Stats Stats;
        public Skills Skills;
        #endregion

        #region Methods
        public string Describe()
        {
            string returnDescription =
                $"\n{Name}, a {Age} year old {Gender}.\n" +
                $"\nTheir stats are:\n\n" +
                Stats.Describe() +
                $"\nTheir skills are:\n\n" +
                Skills.Describe() +
                $"\nThis citizen's ID: {Id}";

            return returnDescription;
        }
        #endregion
    }
}
