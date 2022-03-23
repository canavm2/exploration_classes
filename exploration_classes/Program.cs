using FileTools;
using People;
using Microsoft.Extensions.Configuration;
using Company;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Azure.Cosmos;
//  CL command: dotnet add package Azure.Cosmos 

//used to access user.secrets
var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();




string AzureAccess = config["AzureCSVStorage:AccessApiKey"];
FileTool fileTool = new FileTool(config[AzureAccess]);
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");


//AzureCosmos
string azureUri = config["AzureCosmos:URI"];
string azureKey = config["AzureCosmos:PrimaryKey"];
string databaseId = "testDB";
string containerId = "testContainer";
CosmosClient cosmosClient = new CosmosClient(azureUri, azureKey);
CosmosDatabase database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
CosmosContainer container = await cosmosClient.GetDatabase(databaseId).CreateContainerIfNotExistsAsync(containerId, "/LastName");



IndexId index = new IndexId(fileTool.ReadIndex());
//RelationshipCache relationshipcache = fileTool.ReadRelationshipCache("relationships");
//ModifierList modifierlist = new ModifierList();
//ModifierList modifierlist = fileTool.ReadModifierList("modifierlist");
//TraitList traitlist = new();
//TraitList traitlist = fileTool.ReadTraitList("traitlist");
//PlayerCompany testcompany = fileTool.ReadCompany("company");
//Console.WriteLine(testcompany.Describe());
//CitizenCache citizens = await fileTool.ReadCitizens("citizens", true);

#region createcitizens
//CitizenCache citizens = new CitizenCache(index, 100);
//Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"malecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"nbcitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].DescribeCitizen()}");
#endregion

#region testingmodifiers
//Modifier testmodifier = new("tired", "battle", "stat", "str", -10, true, 1000, "Battle Fatigue");
//testcompany.Advisors["master"].AddModifier(testmodifier);
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
//testcompany.Advisors["master"].RemoveModifier("tired");
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
#endregion

#region createcompany
//List<Citizen> advisors = new List<Citizen>();
//for (int i = 0; i < 7; i++)
//    advisors.Add(citizens.GetRandomCitizen());
//Citizen master = citizens.GetRandomCitizen();
//PlayerCompany testcompany = new("testcompany", index, master, advisors);
#endregion

#region readcompany
//Relationships.UpdateRelationships(testcompany,relationshipcache);
#endregion

#region advisortesting
//Citizen replacementadvisor = citizens.GetRandomCitizen();
//Console.WriteLine(replacementadvisor.Describe());
//Relationships.ReplaceAdvisor(replacementadvisor, testcompany, "advisor1", citizens, relationshipcache);
#endregion

//foreach (Citizen.Trait trait in traitlist.Traits.Values)
//{
//    Console.WriteLine(trait.Summary());
//}
//foreach (Citizen.Trait trait in traitlist.Traits.Values)
//{
//    testcompany.Advisors["master"].RemoveTrait(trait.Name);
//}


//Stores everything again
index.StoreIndex(fileTool);
//await fileTool.StoreCitizens(citizens, "citizens");
//fileTool.StoreCompany(testcompany, "company");
//fileTool.StoreModifierList(modifierlist, "modifierlist");
//fileTool.StoreTraitList(traitlist, "traitlist");
//fileTool.StoreRelationshipCache(relationshipcache, "relationships");