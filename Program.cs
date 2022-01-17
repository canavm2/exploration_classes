﻿using People;
using Company;
using FileTools;


FileTool fileTool = new FileTool();
Console.WriteLine($"The current index is: {fileTool.ReadIndex()}");
IndexId index = new IndexId(fileTool.ReadIndex());
List<Citizen> femaleCitizens = new();
List<Citizen> maleCitizens = new();
NameList nameList = new NameList();

#region createcitizens
for (int i = 0; i < 100; i++)
{
    Citizen newfemale = new(nameList.generateName("female"), "female", index);
    femaleCitizens.Add(newfemale);
}
Console.WriteLine($"Created list of female citizens, there are: {femaleCitizens.Count()} in the list.  The current index is {index.CurrentIndex()}.");
for (int i = 0; i < 100; i++)
{
    Citizen newmale = new(nameList.generateName("male"), "male", index);
    maleCitizens.Add(newmale);
}
Console.WriteLine($"Created list of male citizens, there are: {maleCitizens.Count()} in the list.  The current index is {index.CurrentIndex()}.");
fileTool.StoreCitizens(femaleCitizens, "femaleCitizens");
fileTool.StoreCitizens(maleCitizens, "maleCitizens");
#endregion

#region readcitizens
//femaleCitizens = fileTool.ReadCitizens("femalecitizens");
//maleCitizens = fileTool.ReadCitizens("malecitizens");
//Console.WriteLine($"femalecitizens has: {femaleCitizens.Count} items.");
//Console.WriteLine("The first female is:");
//Console.WriteLine(femaleCitizens[0].Describe());
//Console.WriteLine($"malecitizens has: {maleCitizens.Count} items.");
//Console.WriteLine("The first male is:");
//Console.WriteLine(maleCitizens[0].Describe());
#endregion


//testing modifiers
//Citizen testcitizen = femaleCitizens[0];
//testcitizen.Stats.ApplyModifier("testmodifier", "test", "dex", 4);
//Console.WriteLine(testcitizen.Stats.Modifiers[0].Summary());
//testcitizen.Stats.RemoveModifier("testmodifier-test");


//Stores everything again
index.StoreIndex(fileTool);
//fileTool.StoreCitizens(femaleCitizens, "femaleCitizens");
//fileTool.StoreCitizens(maleCitizens, "maleCitizens");