﻿using Newtonsoft.Json;
using People;
//using System.Text.Json;

namespace FileTools
{
    //An object that gets instatiated, it holds the filepath to the folder everything is saved in.
    public class FileTool
    {
        //TODO better way than instantiating the filetool?
        //I just need to store the filepath, but don't want to do it in every method.
        #region constructor
        //Constructor, needed to store the txt_files folder Path
        public FileTool()
        {

        }
        //used with the System.Text.Json
        //JsonSerializerOptions options = new JsonSerializerOptions { WriteIndented = true };
        string TxtFilePath = @"C:\Users\canav\Documents\learning_dotnet\exploration_classes\txt_files\";
        #endregion

        #region methods
        //All these methods combine the foilder path with the filename provided for reading and writing to the txt files
        public void StoreCitizens(List<Citizen> citizens, string filename)
        {
            filename += ".txt";
            string jsoncitizen = JsonConvert.SerializeObject(citizens);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsoncitizen.ToString());
        }
        public List<Citizen> ReadCitizens(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            List <Citizen> citizens = new List <Citizen>();
            citizens = JsonConvert.DeserializeObject<List<Citizen>>(fileJson);
            return citizens;
        }
        public void StoreIndex(int currentindex)
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            if (currentindex < 100000)
            {
                throw new Exception($"Error: Index too small: {currentindex}");
            }
            string jsoncitizen = JsonConvert.SerializeObject(currentindex);
            File.WriteAllText(filepath, jsoncitizen.ToString());
        }
        public int ReadIndex()
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            string fileJson = File.ReadAllText(filepath);
            int currentindex = 0;
            currentindex = JsonConvert.DeserializeObject<int>(fileJson);
            return currentindex;
        }
        #endregion
    }

    //An object that gets instantiated to holds the current index
    //need to callthe method to get the next index
    public class IndexId
    {
        public IndexId(int index)
        {
            currentindex = index;
        }
        private int currentindex;

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
}
