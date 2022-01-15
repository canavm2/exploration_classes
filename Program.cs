using Citizen;
using Company;


NameList nameList = new();
string randomname = nameList.generateName("female");

CompanyMember citizen = new(randomname, "female");
Console.WriteLine(citizen.Describe());

citizen.Stats.ApplyModifier("Drunk", "event", "socl", -4, true, 1000, "Test Description.");
citizen.Stats.ApplyModifier("Drunk", "event", "socl", -4, true, 1000, "Test Description.");
citizen.Stats.ApplyModifier("Inspiration", "event", "int", 10, true, 180, "Test Description #2.");
foreach (CitizenStats.Modifier modifier in citizen.Stats.Modifiers)
{
    Console.WriteLine(modifier.Summary());
}


PlayerCompany playerCompany = new("Mashers", citizen);