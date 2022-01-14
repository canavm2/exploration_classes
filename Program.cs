using Citizen;
using Company;


NameList nameList = new();
string randomname = nameList.generateName("female");

CompanyMember citizen = new CompanyMember(randomname, "female");
citizen.DescribeCitizen();

CitizenStatModifier TestModifier = new CitizenStatModifier("TestName", "trait", "smt", 4, true, 1000, "Test Description.");
Console.WriteLine(TestModifier.CitizenStatModifierDescription());


PlayerCompany playerCompany = new("Mashers", citizen);