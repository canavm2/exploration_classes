//using Newtonsoft.Json;
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
using Microsoft.Extensions.Configuration;

namespace FileTools
{
    //An object that gets instatiated, it holds the filepath to the folder everything is saved in.
    //It also holds all the methods used to read/write to the .txt files
    public class FileTool
    {
        #region Constructor and Lists
        public FileTool(string azureUri, string azureKey)
        {
            options.WriteIndented = true;
            cosmosClient = new CosmosClient(azureUri, azureKey);
        }
        #endregion

        #region Dictionaries and Properties
        //public string TxtFilePath = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\txt_files\";
        JsonSerializerOptions options = new JsonSerializerOptions();
        string databaseId = "testDB";
        CosmosClient cosmosClient;
        #endregion

        #region methods
        public async Task StoreCitizens(CitizenCache citizens)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<CitizenCache> response = await container.UpsertItemAsync<CitizenCache>(citizens);
        }
        public async Task<CitizenCache> ReadCitizens(string id)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<CitizenCache> response = await container.ReadItemAsync<CitizenCache>(id: id, partitionKey: new PartitionKey(id));
            return (CitizenCache)response;
        }
        public async Task StoreCompany(PlayerCompany playerCompany)
        {
            string containerId = "PlayerCompanies";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<PlayerCompany> response = await container.UpsertItemAsync<PlayerCompany>(playerCompany);
        }
        public async Task<PlayerCompany> ReadCompany(string id)
        {
            string containerId = "PlayerCompanies";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<PlayerCompany> response = await container.ReadItemAsync<PlayerCompany>(id: id, partitionKey: new PartitionKey(id));
            return (PlayerCompany)response;
        }
        public async Task StoreRelationshipCache(RelationshipCache relationships)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<RelationshipCache> response = await container.UpsertItemAsync<RelationshipCache>(relationships);
        }
        public async Task<RelationshipCache> ReadRelationshipCache(string id)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<RelationshipCache> response = await container.ReadItemAsync<RelationshipCache>(id: id, partitionKey: new PartitionKey(id));
            return (RelationshipCache)response;
        }
        public async Task StoreLoadTool(LoadTool loadTool)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<LoadTool> response = await container.UpsertItemAsync<LoadTool>(loadTool);
        }
        public async Task<LoadTool> ReadLoadTool(string id)
        {
            string containerId = "CitizenCache";
            CosmosContainer container = cosmosClient.GetDatabase(databaseId).GetContainer(containerId);
            ItemResponse<LoadTool> response = await container.ReadItemAsync<LoadTool>(id: id, partitionKey: new PartitionKey(id));
            return (LoadTool)response;
        }

        //public void StoreModifierList(ModifierList modifierlist, string filename)
        //{
        //    filename += ".txt";
        //    string jsonmodifierlist = JsonSerializer.Serialize(modifierlist, options);
        //    string filepath = Path.Combine(TxtFilePath, filename);
        //    File.WriteAllText(filepath, jsonmodifierlist);
        //}
        //public ModifierList ReadModifierList(string filename)
        //{
        //    filename += ".txt";
        //    string filepath = Path.Combine(TxtFilePath, filename);
        //    string fileJson = File.ReadAllText(filepath);
        //    ModifierList modifierlist = JsonSerializer.Deserialize<ModifierList>(fileJson);
        //    return modifierlist;
        //}
        //public void StoreTraitList(TraitList traitlist, string filename)
        //{
        //    filename += ".txt";
        //    string jsontraitlist = JsonSerializer.Serialize(traitlist, options);
        //    string filepath = Path.Combine(TxtFilePath, filename);
        //    File.WriteAllText(filepath, jsontraitlist);
        //}
        //public TraitList ReadTraitList(string filename)
        //{
        //    filename += ".txt";
        //    string filepath = Path.Combine(TxtFilePath, filename);
        //    string fileJson = File.ReadAllText(filepath);
        //    TraitList traitlist = JsonSerializer.Deserialize<TraitList>(fileJson);
        //    return traitlist;
        //}
        //public void StoreModifier(Modifier modifier)
        //{
        //    string filepath = Path.Combine(TxtFilePath, "modifier.txt");
        //    string jsoncitizen = JsonSerializer.Serialize(modifier, options);
        //    File.WriteAllText(filepath, jsoncitizen);
        //}
        //public Modifier ReadModifier()
        //{
        //    string filepath = Path.Combine(TxtFilePath, "modifier.txt");
        //    string fileJson = File.ReadAllText(filepath);
        //    Modifier modifier = JsonSerializer.Deserialize<Modifier>(fileJson);
        //    return modifier;
        //}
        #endregion
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
    public class LoadTool
    {
        public LoadTool()
        {
            id = Guid.NewGuid();
            CitizensId = "empty";
            CompanyId = "empty";
            RelationshipId = "empty";
        }

        [JsonConstructor]
        public LoadTool(Guid Id, string citizensId, string companyId, string relationshipId)
        {
            id = Id;
            CitizensId = citizensId;
            CompanyId = companyId;
            RelationshipId = relationshipId;
        }
        public Guid id { get; set; }
        public string CitizensId { get; set; }
        public string CompanyId { get; set; }
        public string RelationshipId { get; set; }
    }
}
