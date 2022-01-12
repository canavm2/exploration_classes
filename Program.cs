using exploration_classes;
Console.WriteLine("Testing");

string path = "names.csv";
List<string> female_names = new List<string>();
List<string> male_names = new List<string>();
List<string> nb_names = new List<string>();
List<string> last_names = new List<string>();

string[] lines = System.IO.File.ReadAllLines(path);
Console.WriteLine(lines);

CompanyMember citizen = new exploration_classes.CompanyMember();
citizen.describeCitizen();


PlayerCompany playerCompany = new PlayerCompany("Mashers", citizen);