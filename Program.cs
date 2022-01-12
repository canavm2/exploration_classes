using Citizen;
using Company;


NameList nameList = new();
string randomname = nameList.generateName("female");

CompanyMember citizen = new CompanyMember(randomname, "female");
citizen.DescribeCitizen();


PlayerCompany playerCompany = new("Mashers", citizen);