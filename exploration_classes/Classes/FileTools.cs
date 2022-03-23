﻿//using Newtonsoft.Json;
using People;
using Company;
using Relation;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Azure.Cosmos;
//using System.Text.Json;

namespace FileTools
{
    //An object that gets instatiated, it holds the filepath to the folder everything is saved in.
    //It also holds all the methods used to read/write to the .txt files
    public class FileTool
    {
        #region Constructor and Lists
        public FileTool(string accessKey)
        {
            BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=exploration202203;AccountKey=" + accessKey + ";EndpointSuffix=core.windows.net";
            //creates the container, which is like a folder on blob storage
            container = new BlobContainerClient(BlobStorageConnectionString, ExplorationTXTContainerName);
            options.WriteIndented = true;
        }
        BlobContainerClient container;
        string BlobStorageConnectionString;
        string ExplorationTXTContainerName = "explorationtxt";
        public string TxtFilePath = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\txt_files\";
        JsonSerializerOptions options = new JsonSerializerOptions();
        
        #endregion

        #region methods

        public async Task<string> ReadTest()
        {
            //blobclient is the file
            BlobClient blob = container.GetBlobClient("testtxt.txt");
            if (await blob.ExistsAsync())
            {
                BlobDownloadInfo download = await blob.DownloadAsync();
                byte[] result = new byte[download.ContentLength];
                await download.Content.ReadAsync(result, 0, (int)download.ContentLength);

                return Encoding.UTF8.GetString(result);
            }
            return "error, read didnt happen";

        }

        public async Task StoreTxt(string jsonInfo, string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsonInfo);
            BlobClient blobClient = container.GetBlobClient(filename);
            using FileStream uploadFileStream = File.OpenRead(filepath);
            await blobClient.UploadAsync(uploadFileStream, true);
            uploadFileStream.Close();
        }

        public string ReadTxt(string filename)
        {
            string filepath = Path.Combine(TxtFilePath, filename);
            string infoJson = File.ReadAllText(filepath);
            return infoJson;
        }

        public async Task StoreCitizens(CitizenCache citizens, string filename)
        {
            string jsoncitizen = JsonSerializer.Serialize(citizens, options);
            await StoreTxt(jsoncitizen, filename);
        }
        public CitizenCache ReadCitizens(string filename)
        {
            filename += ".txt";
            string infoJson = ReadTxt(filename);
            CitizenCache citizens = JsonSerializer.Deserialize<CitizenCache>(infoJson);
            return citizens;
        }
        public void StoreCompany(PlayerCompany playercompany, string filename)
        {
            filename += ".txt";
            string jsoncompany = JsonSerializer.Serialize(playercompany, options);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsoncompany);
        }
        public PlayerCompany ReadCompany(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            Console.WriteLine(fileJson);
            PlayerCompany playercompany = JsonSerializer.Deserialize<PlayerCompany>(fileJson);
            return playercompany;
        }
        public void StoreRelationshipCache(RelationshipCache relationships, string filename)
        {
            filename += ".txt";
            string jsonrelationshipcache = JsonSerializer.Serialize(relationships, options);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsonrelationshipcache);
        }
        public RelationshipCache ReadRelationshipCache(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            RelationshipCache relationshipcache = JsonSerializer.Deserialize<RelationshipCache>(fileJson);
            return relationshipcache;
        }
        public void StoreModifierList(ModifierList modifierlist, string filename)
        {
            filename += ".txt";
            string jsonmodifierlist = JsonSerializer.Serialize(modifierlist, options);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsonmodifierlist);
        }
        public ModifierList ReadModifierList(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            ModifierList modifierlist = JsonSerializer.Deserialize<ModifierList>(fileJson);
            return modifierlist;
        }
        public void StoreTraitList(TraitList traitlist, string filename)
        {
            filename += ".txt";
            string jsontraitlist = JsonSerializer.Serialize(traitlist, options);
            string filepath = Path.Combine(TxtFilePath, filename);
            File.WriteAllText(filepath, jsontraitlist);
        }
        public TraitList ReadTraitList(string filename)
        {
            filename += ".txt";
            string filepath = Path.Combine(TxtFilePath, filename);
            string fileJson = File.ReadAllText(filepath);
            TraitList traitlist = JsonSerializer.Deserialize<TraitList>(fileJson);
            return traitlist;
        }
        public void StoreIndex(int currentindex)
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            if (currentindex < 100000)
            {
                throw new Exception($"Error: Index too small: {currentindex}");
            }
            string jsoncitizen = JsonSerializer.Serialize(currentindex);
            File.WriteAllText(filepath, jsoncitizen);
        }
        public int ReadIndex()
        {
            string filepath = Path.Combine(TxtFilePath, "index.txt");
            string fileJson = File.ReadAllText(filepath);
            int currentindex = 0;
            currentindex = JsonSerializer.Deserialize<int>(fileJson);
            return currentindex;
        }
        public void StoreModifier(Modifier modifier)
        {
            string filepath = Path.Combine(TxtFilePath, "modifier.txt");
            string jsoncitizen = JsonSerializer.Serialize(modifier, options);
            File.WriteAllText(filepath, jsoncitizen);
        }
        public Modifier ReadModifier()
        {
            string filepath = Path.Combine(TxtFilePath, "modifier.txt");
            string fileJson = File.ReadAllText(filepath);
            Modifier modifier = JsonSerializer.Deserialize<Modifier>(fileJson);
            return modifier;
        }
        #endregion
    }

    public class AzureFileTool
    {
        #region Constructor and Lists
        public AzureFileTool(string accessKey)
        {
            BlobStorageConnectionString = "DefaultEndpointsProtocol=https;AccountName=exploration202203;AccountKey=" + accessKey + ";EndpointSuffix=core.windows.net";
            //creates the container, which is like a folder on blob storage
            container = new BlobContainerClient(BlobStorageConnectionString, ExplorationTXTContainerName);
        }
        BlobContainerClient container;
        string BlobStorageConnectionString;
        string ExplorationTXTContainerName = "explorationtxt";
        #endregion




        //NEED TO do this in filetool and then get rid of azure filetool  TODO!!!!!!
        public async Task<string> AzureReadCitizens(string filename)
        {
            filename += ".txt";
            //blobclient is the file
            BlobClient blob = container.GetBlobClient(filename);
            if (await blob.ExistsAsync())
            {
                BlobDownloadInfo download = await blob.DownloadAsync();
                byte[] result = new byte[download.ContentLength];
                await download.Content.ReadAsync(result, 0, (int)download.ContentLength);
                return Encoding.UTF8.GetString(result);
            }
            else throw new Exception();

        }
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
