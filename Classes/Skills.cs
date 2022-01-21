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
            //Stores all the Vocational Skills
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
            //Stores all the Experiential Skills
            List<string> ExpSkillsList = new() { "exp1", "exp2", "exp3" };

            //Sets all the Vocational skills to a random value between 0 and 10
            foreach (string skill in VocSkillsList)
            {
                if (type == "company") VocSkill[skill] = 0;
                else VocSkill[skill] = 0;//random.Next(0, 10);
            }

            //Picks 1 Voc Skill to set between 30 and 40, and 4 others between 15-25
            //then overwrites the original values
            if (type != "company")
            {
                List<string> tempVocSkillsList = VocSkillsList;
                int index = random.Next(tempVocSkillsList.Count);
                string highskill = tempVocSkillsList[index];
                tempVocSkillsList.RemoveAt(index);
                VocSkill[highskill] = random.Next(30, 40);
                for (int i = 0; i < 4; i++)
                {
                    index = random.Next(tempVocSkillsList.Count);
                    highskill = tempVocSkillsList[index];
                    tempVocSkillsList.RemoveAt(index);
                    VocSkill[highskill] = random.Next(15, 25);
                }
            }

            //Sets all the experiential skills between 0 and 10
            foreach (string skill in ExpSkillsList)
            {
                if (type == "company") ExpSkill[skill] = 0;
                else ExpSkill[skill] = 0;// random.Next(0, 10);
            }
        }


        [JsonConstructor]
        public Skills(Dictionary<string, int> vocskill, Dictionary<string, int> expskill)
        {
            VocSkill = vocskill;
            ExpSkill = expskill;
        }
        #endregion

        #region Dictionaries and Properties
        public Dictionary<string, int> VocSkill = new();
        public Dictionary<string, int> ExpSkill = new();
        #endregion


        public string Describe()
        {
            //Iterates over all the Skills, and provides a string that describes it
            string vocDesc = "";
            foreach (KeyValuePair<string, int> skill in VocSkill)
            {
                if(skill.Value > 0)
                {
                    string tempDesc = $"{skill.Key}: {skill.Value.ToString()}\n";
                    vocDesc += tempDesc;
                }
            }
            string expDesc = "";
            foreach (KeyValuePair<string, int> skill in ExpSkill)
            {
                if (skill.Value > 0)
                {
                    string tempDesc = $"{skill.Key}: {skill.Value.ToString()}\n";
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

    }
}
