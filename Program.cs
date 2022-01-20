using People;
using Company;
using FileTools;
using Relationships;
using Newtonsoft.Json;


FileTool fileTool = new FileTool();
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");
IndexId index = new IndexId(fileTool.ReadIndex());
RelationshipCache RelationshipCache = new();
CitizenCache citizens;

#region createcitizens
//citizens = new CitizenCache(index, 100);
//fileTool.StoreCitizens(citizens, "citizens");
#endregion

#region readcitizens
citizens = fileTool.ReadCitizens("citizens");
Console.WriteLine($"femalecitizens has: {citizens.FemaleCitizens.Count} items.\nThe first female is:\n{citizens.FemaleCitizens[0].Describe()}");
Console.WriteLine($"femalecitizens has: {citizens.MaleCitizens.Count} items.\nThe first male is:\n{citizens.MaleCitizens[0].Describe()}");
Console.WriteLine($"femalecitizens has: {citizens.NBCitizens.Count} items.\nThe first non-binary is:\n{citizens.NBCitizens[0].Describe()}");
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

#region companies
//Random random = new Random();
//int randomindex = random.Next(citizens.FemaleCitizens.Count);
//Citizen Master = citizens.FemaleCitizens[randomindex];
////femaleCitizens.RemoveAt(randomindex);
//List<Citizen> Advisors = new();
//for (int i = 0; i < 7; i++)
//{
//    randomindex = random.Next(citizens.FemaleCitizens.Count);
//    Advisors.Add(citizens.FemaleCitizens[randomindex]); //as Citizen;
//    //femaleCitizens.RemoveAt(randomindex);
//}
//PlayerCompany testcompany = new("testcompany", index, Master, Advisors);
//Console.WriteLine(testcompany.Describe());
//Console.WriteLine($"There are {testcompany.Social.Relationships.Count} relationships.");
//Citizen replacementCitizen = citizens.FemaleCitizens[random.Next(citizens.FemaleCitizens.Count)];
//Console.WriteLine(replacementCitizen.Describe());
////testcompany.ReplaceAdvisor(replacementCitizen, "advisor1");
//testcompany.UpdateSocial();
//Console.WriteLine(testcompany.Describe());

//foreach (KeyValuePair<string, Citizen> citizen in testcompany.Advisors)
//{
//    Console.WriteLine($"\n------------------\nThis citizen is the: {citizen.Key}");
//    Console.WriteLine(citizen.Value.Describe());
//}
//fileTool.StoreCompany(testcompany, "company");
//PlayerCompany testcompany = fileTool.ReadCompany("company");
//testcompany.UpdateSocial();
//Console.WriteLine("======================================================");
//Console.WriteLine("======================================================");
//Console.WriteLine("======================================================");
//foreach (KeyValuePair<string, Citizen> citizen in testcompany.Advisors)
//{
//    Console.WriteLine($"\n------------------\nThis citizen is the: {citizen.Key}");
//    Console.WriteLine(citizen.Value.Describe());
//}
#endregion

//Stores everything again
index.StoreIndex(fileTool);
fileTool.StoreCitizens(citizens, "citizens");