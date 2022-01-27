using Newtonsoft.Json;
using People;
using Company;
using Relation;
//using System.Text.Json;

namespace FileTools
{
    //An object that gets instatiated, it holds the filepath to the folder everything is saved in.
    //It also holds all the methods used to read/write to the .txt files
    public class FileTool
    {
        #region Constructor and Lists
        public FileTool() { }
        string TxtFilePath = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\txt_files\";
        #endregion

        #region methods
        public void StoreCitizens(CitizenCache citizens, string filename)
        {
            filename += ".txt";
            string jsoncitizen = JsonConvert.SerializeObject(citizens, Formatting.Indented);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsoncitizen);
        }
        public CitizenCache ReadCitizens(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            CitizenCache citizens;
            citizens = JsonConvert.DeserializeObject<CitizenCache>(fileJson);
            return citizens;
        }
        public void StoreCompany(PlayerCompany playercompany, string filename)
        {
            filename += ".txt";
            string jsoncompany = JsonConvert.SerializeObject(playercompany, Formatting.Indented);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsoncompany);
        }
        public PlayerCompany ReadCompany(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            PlayerCompany playercompany = JsonConvert.DeserializeObject<PlayerCompany>(fileJson);
            return playercompany;
        }
        public void StoreRelationshipCache(RelationshipCache relationships, string filename)
        {
            filename += ".txt";
            string jsonrelationshipcache = JsonConvert.SerializeObject(relationships, Formatting.Indented);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsonrelationshipcache);
        }
        public RelationshipCache ReadRelationshipCache(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            RelationshipCache relationshipcache = JsonConvert.DeserializeObject<RelationshipCache>(fileJson);
            return relationshipcache;
        }
        public void StoreModifierList(ModifierList modifierlist, string filename)
        {
            filename += ".txt";
            string jsonmodifierlist = JsonConvert.SerializeObject(modifierlist, Formatting.Indented);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsonmodifierlist);
        }
        public ModifierList ReadModifierList(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            ModifierList modifierlist = JsonConvert.DeserializeObject<ModifierList>(fileJson);
            return modifierlist;
        }
        public void StoreTraitList(TraitList traitlist, string filename)
        {
            filename += ".txt";
            string jsontraitlist = JsonConvert.SerializeObject(traitlist, Formatting.Indented);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsontraitlist);
        }
        public TraitList ReadTraitList(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            TraitList traitlist = JsonConvert.DeserializeObject<TraitList>(fileJson);
            return traitlist;
        }
        public void StoreIndex(int currentindex)
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            if (currentindex < 100000)
            {
                throw new Exception($"Error: Index too small: {currentindex}");
            }
            string jsoncitizen = JsonConvert.SerializeObject(currentindex);
            File.WriteAllText(filepath, jsoncitizen);
        }
        public int ReadIndex()
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            string fileJson = File.ReadAllText(filepath);
            int currentindex = 0;
            currentindex = JsonConvert.DeserializeObject<int>(fileJson);
            return currentindex;
        }
        public void StoreModifier(Modifier modifier)
        {
            string filepath = Path.Combine(TxtFilePath, "modifier.txt");
            string jsoncitizen = JsonConvert.SerializeObject(modifier, Formatting.Indented);
            File.WriteAllText(filepath, jsoncitizen);
        }
        public Modifier ReadModifier()
        {
            string filepath = Path.Combine(TxtFilePath, "modifier.txt");
            string fileJson = File.ReadAllText(filepath);
            Modifier modifier = JsonConvert.DeserializeObject<Modifier>(fileJson);
            return modifier;
        }
        #endregion
    }

    //An object that gets instantiated to holds the current index and method to call the next index.
    public class IndexId
    {
        public IndexId(int index)
        {
            currentindex = index;
        }
        private int currentindex;

        //method to call to get the next unused index
        public int GetIndex()
        {
            currentindex++;
            return currentindex;
        }
        public string CurrentIndex()
        {
            //converts to a string so it isnt used as an indexer.
            return ("!"+ currentindex.ToString() + "!");
        }
        public void StoreIndex(FileTool filetool)
        {
            filetool.StoreIndex(currentindex);
        }
    }

    //An object that can be isntatiated to hold the lists of skills, stats, and attributes
    public class ListTool
    {
        public ListTool(){}
        public List<string> VocSkillsList = new()
        {
            "Academia",
            "Athletics",
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

        public List<string> ExpSkillsList = new() { "exp1", "exp2", "exp3" };
        public List<string> PrimaryStats = new() { "STR", "DEX", "INT", "WIS", "CHA", "LDR" };
        public List<string> DerivedStats = new() { "PHYS", "MNTL", "SOCL" };
        public List<string> Attributes = new List<string>() {
                "Health",
                "Happiness",
                "Motivation",
                "Psyche"
            };
    }
}
