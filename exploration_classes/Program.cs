using FileTools;
using People;
using Microsoft.Extensions.Configuration;
using Company;
using Relation;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Azure.Cosmos;
//  CL command: dotnet add package Azure.Cosmos 

//used to access user.secrets
var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();
string azureUri = config["AzureCosmos:URI"];
string azureKey = config["AzureCosmos:PrimaryKey"];
string citizensId = "59558795-f812-4258-90bd-c4bdd0f9ddf4";
string companyId = "3c29a4b7-ad9d-414d-9124-e7e09ab9f699";
string relationshipId = "4938643c-31b8-4e8c-9ff8-0816c09904da";


//FileTool fileTool = new FileTool(azureUri, azureKey);

//CitizenCache citizens = await fileTool.ReadCitizens(citizensId);
//Console.WriteLine("Female Citizen[0] age is: " + citizens.FemaleCitizens[0].Age);
//citizens.FemaleCitizens[0].Age += 1;

//Console.WriteLine("Female Citizen[0] age is now: " + citizens.FemaleCitizens[0].Age);
//PlayerCompany testcompany = await fileTool.ReadCompany(companyId);
//Console.WriteLine(testcompany.Describe());

//RelationshipCache relationshipCache = await fileTool.ReadRelationshipCache(relationshipId);
//Console.WriteLine(relationshipCache.id);



#region azurecosmos
//string databaseId = "testDB";
//string containerId = "testContainer";
//CosmosClient cosmosClient = new CosmosClient(azureUri, azureKey);
//CosmosDatabase database = await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
//CosmosContainer container = await cosmosClient.GetDatabase(databaseId).CreateContainerIfNotExistsAsync(containerId, "/LastName");
#endregion


//RelationshipCache relationshipcache = fileTool.ReadRelationshipCache("relationships");
//ModifierList modifierlist = new ModifierList();
//ModifierList modifierlist = fileTool.ReadModifierList("modifierlist");
//TraitList traitlist = new();
//TraitList traitlist = fileTool.ReadTraitList("traitlist");

#region createcitizens
//CitizenCache citizens = new CitizenCache(100);
//Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"malecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].DescribeCitizen()}");
//Console.WriteLine($"nbcitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].DescribeCitizen()}");
#endregion

#region createcompany
//List<Citizen> advisors = new List<Citizen>();
//for (int i = 0; i < 7; i++)
//    advisors.Add(citizens.GetRandomCitizen());
//Citizen master = citizens.GetRandomCitizen();
//PlayerCompany testcompany = new("testcompany", master, advisors);
#endregion

#region testingmodifiers
//Modifier testmodifier = new("tired", "battle", "stat", "str", -10, true, 1000, "Battle Fatigue");
//testcompany.Advisors["master"].AddModifier(testmodifier);
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
//testcompany.Advisors["master"].RemoveModifier("tired");
//Console.WriteLine(testcompany.Advisors["master"].DescribeCitizen());
#endregion

#region advisortesting
//Citizen replacementadvisor = citizens.GetRandomCitizen();
//Console.WriteLine(replacementadvisor.Describe());
//Relationships.ReplaceAdvisor(replacementadvisor, testcompany, "advisor1", citizens, relationshipcache);
#endregion


//Stores everything again
//await fileTool.StoreCitizens(citizens);
//await fileTool.StoreCompany(testcompany);
//await fileTool.StoreRelationshipCache(relationshipCache);
//fileTool.StoreModifierList(modifierlist, "modifierlist");
//fileTool.StoreTraitList(traitlist, "traitlist");
//fileTool.StoreRelationshipCache(relationshipcache, "relationships");





