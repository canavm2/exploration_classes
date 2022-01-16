using People;
using Company;
using FileTools;


//NameList nameList = new();

//Citizen citizen1 = new(nameList.generateName("female"), "female");
//Citizen citizen2 = new(nameList.generateName("male"), "male");
//Citizen citizen3 = new(nameList.generateName("female"), "female");
//Citizen citizen4 = new(nameList.generateName("male"), "male");

//List<Citizen> citizens = new() {citizen1, citizen2, citizen3, citizen4};
//FileTool.StoreCitizens(citizens, "citizenstest");
List<Citizen> citizens = FileTool.ReadCitizens("citizenstest");
foreach (Citizen c in citizens)
{
    Console.WriteLine(c.Describe());
}


//citizen1.Stats.ApplyModifier("Drunk", "event", "socl", -4, true, 1000, "Test Description.");
//citizen1.Stats.ApplyModifier("Drunk", "event", "socl", -4, true, 1000, "Test Description.");
//citizen1.Stats.ApplyModifier("Inspiration", "event", "int", 10, true, 180, "Test Description #2.");
//foreach (CitizenStats.Modifier modifier in citizen1.Stats.Modifiers)
//{
//    Console.WriteLine(modifier.Summary());
//}

