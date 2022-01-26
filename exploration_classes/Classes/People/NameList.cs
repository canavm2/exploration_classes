using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    internal class NameList
    {
        public NameList()
        {
            // Reads a CSV file and imports 4 list of random names, female, male, non-binary, and last.
            // Saves the names to 4 lists of strings//
            // string path = "C:/Users/canav/Documents/VS_Projects/exploration_classes/names.csv";
            string path = @"C:\Users\canav\Documents\ExplorationProject\exploration_classes\txt_files\names.csv";
            string[] lines = System.IO.File.ReadAllLines(path);
            string[] female_array = lines[0].Split(',');
            string[] male_array = lines[1].Split(',');
            string[] nb_array = lines[2].Split(',');
            string[] last_array = lines[3].Split(',');

            // Saves the names to 4 lists of strings
            female_names = new List<string>(female_array);
            male_names = new List<string>(male_array);
            nb_names = new List<string>(nb_array);
            last_names = new List<string>(last_array);
        }

        List<string> female_names = new();
        List<string> male_names = new();
        List<string> nb_names = new();
        List<string> last_names = new();

        public string generateName(string type)
        {
            Random random = new();
            int index = random.Next(last_names.Count);
            string firstName;
            string lastName = last_names[index];
            string fullName;
            if (type == "female")
            {
                index = random.Next(female_names.Count);
                firstName = female_names[index];
            }
            else if (type == "male")
            {
                index = random.Next(male_names.Count);
                firstName = male_names[index];
            }
            else
            {
                index = random.Next(nb_names.Count);
                firstName = nb_names[index];
            }
            fullName = firstName + " " + lastName;
            return fullName;
        }
    }
}
