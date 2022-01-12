using exploration_classes;

string path = "C:/Users/canav/Documents/VS_Projects/exploration_classes/names.csv";


string[] lines = System.IO.File.ReadAllLines(path);
string[] female_array = lines[0].Split(',');
string[] male_array = lines[1].Split(',');
string[] nb_array = lines[2].Split(',');
string[] last_array = lines[3].Split(',');

List<string> female_names = new List<string>(female_array);
List<string> male_names = new List<string>(male_array);
List<string> nb_names = new List<string>(nb_array);
List<string> last_names = new List<string>(last_array);



CompanyMember citizen = new exploration_classes.CompanyMember();
citizen.describeCitizen();


PlayerCompany playerCompany = new PlayerCompany("Mashers", citizen);