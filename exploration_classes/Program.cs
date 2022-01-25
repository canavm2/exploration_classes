using People;
using Company;
using FileTools;
using Relation;
using Newtonsoft.Json;


FileTool fileTool = new FileTool();
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");
IndexId index = new IndexId(fileTool.ReadIndex());
//RelationshipCache relationshipcache;
CitizenCache citizens;

//PlayerCompany testcompany = fileTool.ReadCompany("company");
//Console.WriteLine(testcompany.Describe());
citizens = fileTool.ReadCitizens("citizens");
//relationshipcache = fileTool.ReadRelationshipCache("relationships");

#region createcitizens
//citizens = new CitizenCache(index, 100);
#endregion

#region readcitizens
Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].Describe()}");
Console.WriteLine($"malecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].Describe()}");
Console.WriteLine($"nbcitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].Describe()}");
#endregion

#region testingmodifiers
//Citizen testcitizen = femaleCitizens[0];
//Console.WriteLine(testcitizen.Describe());
//testcitizen.Stats.ApplyModifier("testmodifier", "test", "dex", 4, true, 10000, "does this work");
//Console.WriteLine(testcitizen.Stats.Modifiers[0].Summary());
//fileTool.StoreModifier(testcitizen.Stats.Modifiers[0]);
//string modifiedstat = fileTool.ReadModifier().ModifiedStat;
//Console.WriteLine($"The modified stat is: {modifiedstat}.");

//Console.WriteLine(fileTool.ReadModifier().Description);
//Console.WriteLine(testcitizen.Stats.Modifiers[0].Summary());
//testcitizen.Stats.RemoveModifier("testmodifier-test");
#endregion

#region createcompany
List<Citizen> advisors = new List<Citizen>();
for (int i = 0; i < 7; i++)
    advisors.Add(citizens.GetRandomCitizen());
Citizen master = citizens.GetRandomCitizen();
PlayerCompany testcompany = new("testcompany", index, master, advisors);
#endregion

#region readcompany
//Relationships.UpdateRelationships(testcompany,relationshipcache);
#endregion

#region advisortesting
//Citizen replacementadvisor = citizens.GetRandomCitizen();
//Console.WriteLine(replacementadvisor.Describe());
//Relationships.ReplaceAdvisor(replacementadvisor, testcompany, "advisor1", citizens, relationshipcache);
#endregion

//Stores everything again
index.StoreIndex(fileTool);
fileTool.StoreCitizens(citizens, "citizens");
fileTool.StoreCompany(testcompany, "company");
//fileTool.StoreRelationshipCache(relationshipcache, "relationships");