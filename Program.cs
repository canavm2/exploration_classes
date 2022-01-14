using Citizen;
using Company;


NameList nameList = new();
string randomname = nameList.generateName("female");

CompanyMember citizen = new(randomname, "female");
citizen.DescribeCitizen();

CitizenStatModifier TestModifier = new CitizenStatModifier("TestName", "trait", "socl", -4, true, 1000, "Test Description.");
//Console.WriteLine(TestModifier.CitizenStatModifierDescription());
citizen.Stats.ApplyModifier(TestModifier);


PlayerCompany playerCompany = new("Mashers", citizen);