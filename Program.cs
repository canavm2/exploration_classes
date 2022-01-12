using exploration_classes;
Console.WriteLine("Testing");

CompanyMember citizen = new exploration_classes.CompanyMember();
citizen.describeCitizen();
List<string> female_names = new List<string>()
{
    "ann","catie"
};

PlayerCompany playerCompany = new PlayerCompany("Mashers", citizen);