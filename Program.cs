using People;
using Company;
using FileTools;
using Newtonsoft.Json;


FileTool fileTool = new FileTool();
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");
IndexId index = new IndexId(fileTool.ReadIndex());
List<Citizen> femaleCitizens = new();
List<Citizen> maleCitizens = new();
NameList nameList = new NameList();
Social social = new Social();

#region createcitizens
//for (int i = 0; i < 100; i++)
//{
//    Citizen newfemale = new(nameList.generateName("female"), "female", index);
//    femaleCitizens.Add(newfemale);
//}
//Console.WriteLine($"Created list of female citizens, there are: {femaleCitizens.Count()} in the list.  The current index is {index.CurrentIndex()}.");
//for (int i = 0; i < 100; i++)
//{
//    Citizen newmale = new(nameList.generateName("male"), "male", index);
//    maleCitizens.Add(newmale);
//}
//Console.WriteLine($"Created list of male citizens, there are: {maleCitizens.Count()} in the list.  The current index is {index.CurrentIndex()}.");
//fileTool.StoreCitizens(femaleCitizens, "femaleCitizens");
//fileTool.StoreCitizens(maleCitizens, "maleCitizens");
#endregion

#region readcitizens
femaleCitizens = fileTool.ReadCitizens("femalecitizens");
maleCitizens = fileTool.ReadCitizens("malecitizens");
//Console.WriteLine($"femalecitizens has: {femaleCitizens.Count} items.");
//Console.WriteLine("The first female is:");
//Console.WriteLine(femaleCitizens[0].Describe());
//Console.WriteLine($"malecitizens has: {maleCitizens.Count} items.");
//Console.WriteLine("The first male is:");
//Console.WriteLine(maleCitizens[0].Describe());
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
Random random = new Random();
int randomindex = random.Next(femaleCitizens.Count);
Citizen Master = femaleCitizens[randomindex];
//femaleCitizens.RemoveAt(randomindex);
List<Citizen> Advisors = new();
for (int i = 0; i < 7; i++)
{
    randomindex = random.Next(femaleCitizens.Count);
    Advisors.Add(femaleCitizens[randomindex]); //as Citizen;
    //femaleCitizens.RemoveAt(randomindex);
}
PlayerCompany testcompany = new("testcompany", index, Master, Advisors);
Console.WriteLine(testcompany.Describe());
Console.WriteLine($"There are {testcompany.Social.Relationships.Count} relationships.");

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
fileTool.StoreCitizens(femaleCitizens, "femaleCitizens");
fileTool.StoreCitizens(maleCitizens, "maleCitizens");
fileTool.StoreCompany(testcompany, "company");