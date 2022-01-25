using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace People
{
    public class Skills
    {
        #region Constructors
        public Skills(string type = "citizen")
        {
            Random random = new();
            Modifiers = new();
            List<string> VocSkillsList = new()
            {
                "Academia",
                "Animal Handling",
                "Blacksmithing",
                "Carpentry",
                "Cooking",
                "Diplomacy",
                "Drill",
                "Engineering",
                "First Aid",
                "History",
                "Hunting",
                "Law",
                "Leadership",
                "Leatherworking",
                "Martial",
                "Medical",
                "Metalworking",
                "Pathfinding",
                "Persuation",
                "Politics",
                "Prospecting",
                "Refining",
                "Quartermastery",
                "Skullduggery",
                "Stealth",
                "Survival",
                "Tactics",
                "Tinker"
            };
            List<string> ExpSkillsList = new() { "exp1", "exp2", "exp3" };

            foreach (string skill in VocSkillsList)
            {
                if (type == "company") VocSkill[skill] = new(0);
                else VocSkill[skill] = new(0);//random.Next(0, 10);
            }

            if (type != "company")
            {
                List<string> tempVocSkillsList = VocSkillsList;
                int index = random.Next(tempVocSkillsList.Count);
                string highskill = tempVocSkillsList[index];
                tempVocSkillsList.RemoveAt(index);
                VocSkill[highskill] = new(random.Next(30, 40));
                for (int i = 0; i < 4; i++)
                {
                    index = random.Next(tempVocSkillsList.Count);
                    highskill = tempVocSkillsList[index];
                    tempVocSkillsList.RemoveAt(index);
                    VocSkill[highskill] = new(random.Next(15, 25));
                }
            }

            //Sets all the experiential skills between 0 and 10
            foreach (string skill in ExpSkillsList)
            {
                if (type == "company") ExpSkill[skill] = new(0);
                else ExpSkill[skill] = new(0);// random.Next(0, 10);
            }
        }


        [JsonConstructor]
        public Skills(Dictionary<string, Skill> vocskill, Dictionary<string, Skill> expskill, List<Modifier> modifiers)
        {
            VocSkill = vocskill;
            ExpSkill = expskill;
            Modifiers = modifiers;
        }
        #endregion

        #region Dictionaries and Properties
        public Dictionary<string, Skill> VocSkill = new();
        public Dictionary<string, Skill> ExpSkill = new();
        public List<Modifier> Modifiers;
        #endregion

        #region Methods
        public string Describe()
        {
            //Iterates over all the Skills, and provides a string that describes it
            string vocDesc = "";
            foreach (KeyValuePair<string, Skill> skill in VocSkill)
            {
                if(skill.Value.Full > 0)
                {
                    string tempDesc = $"{skill.Key}: {skill.Value.Full.ToString()}\n";
                    vocDesc += tempDesc;
                }
            }
            string expDesc = "";
            foreach (KeyValuePair<string, Skill> skill in ExpSkill)
            {
                if (skill.Value.Full > 0)
                {
                    string tempDesc = $"{skill.Key}: {skill.Value.Full.ToString()}\n";
                    expDesc += tempDesc;
                }
            }
            string description =
                $"Vocational Skills:\n" +
                vocDesc +
                $"\nExperiential Skills:\n" +
                expDesc;
            return description;
        }
        #endregion

    }

    #region subclasses
    public class Skill
    {
        public Skill(int unmod)
        {
            Full = unmod;
            Unmodified = unmod;
        }

        [JsonConstructor]
        public Skill(int full, int unmodified)
        {
            Full=full;
            Unmodified=unmodified;
        }
        public int Full;
        public int Unmodified;
    }

    //public class Modifier
    //{
    //    // IMPORTANT: Json Deserialization uses the name of the property as the parameter
    //    // if the property is readonly it must match or it will not be able to change it after the constructor
    //    public Modifier(string name, string source, string modifiedskill, int value, bool temporary, int duration, string description)
    //    {
    //        Name = name;
    //        Description = description;
    //        Source = source;
    //        ModifiedSkill = modifiedskill;
    //        Value = value;
    //        Temporary = temporary;
    //        Duration = duration;
    //        //TODO change exception
    //        if (duration < 0) throw new Exception($"Negative Duration: {duration}");
    //        Id = name + "-" + source;
    //    }

    //    public readonly string Name;
    //    public readonly string Description;
    //    public readonly string Source;
    //    public readonly string ModifiedSkill;
    //    public readonly int Value;
    //    public readonly bool Temporary;
    //    public readonly int Duration;
    //    public readonly string Id;

    //    public string Summary()
    //    {
    //        string returnSummary = $"Citizen Stat Modifier: {Name}\n" +
    //            $"{ModifiedSkill}: {Value}\n" +
    //            $"Description: {Description}\n" +
    //            $"ID: {Id}";
    //        if (Temporary)
    //            returnSummary = returnSummary + $"\n" +
    //                $"Duration: {Duration}\n";
    //        return returnSummary;
    //    }
    //}
    #endregion
}
