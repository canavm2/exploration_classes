using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using Newtonsoft.Json;
using FileTools;

namespace FileTools
{
    public class TraitList
    {
        public TraitList()
        {
            ListTool listTool = new();
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i = 1; i < lines.Length; i++)
            {
                List<Modifier> modifiers = new();
                string[] line = lines[i].Split(",");
                for (int j = 2; j < line.Length; j++)
                {
                    string[] split = line[j].Split(".");
                    string modifiedvalue = split[0];
                    int value = int.Parse(split[1]);
                    string type = "";
                    if (listTool.primaryStats.Contains(modifiedvalue)) type = "stat";
                    if (listTool.VocSkillsList.Contains(modifiedvalue)) type = "skill";
                    if (listTool.primaryStats.Contains(modifiedvalue)) type = "attribute";
                    string description = $"{modifiedvalue} {value}";

                    modifiers.Add(new Modifier(line[j], "trait", type, modifiedvalue, value, description));
                }
                Citizen.Trait trait = new Citizen.Trait(line[0],Int32.Parse(line[1]),modifiers);
                Traits[line[0]] = trait;
            }
        }

        [JsonConstructor]
        public TraitList(Dictionary<string, Citizen.Trait> traits)
        {
            Traits = traits;
        }

        public Dictionary<string, Citizen.Trait> Traits = new();
        string path = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\csv_files\traits.csv";
    }

    public class ModifierList
    {
        public ModifierList()
        {
            string[] lines = System.IO.File.ReadAllLines(path);
            for (int i= 1; i < lines.Length; i++)
            {
                string[] line = lines[i].Split(",");
                Modifier modifier = new(
                    line[0],
                    line[1],
                    line[2],
                    line[3],
                    Int32.Parse(line[4]),
                    line[7],
                    Convert.ToBoolean(line[5]),
                    Int32.Parse(line[6])                    
                    );
                Modifiers[line[0]] = modifier;
            }
        }

        [JsonConstructor]
        public ModifierList(Dictionary<string, Modifier> modifiers)
        {
            Modifiers = modifiers;
        }

        public Dictionary<string, Modifier> Modifiers = new();
        string path = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\csv_files\modifiers.csv";
    }
}
