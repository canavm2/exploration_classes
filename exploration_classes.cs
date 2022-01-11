using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploration_classes
{
    public class Citizen
    {
        private Random random = new Random(); 
        public Citizen(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";
        }

        public Citizen()
        {
            Name = "John Doe";
            Age = random.Next(12, 50);
            Gender = "male";
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        private List<string> genders = new List<string>() { "male", "female", "non-binary" };


    }
}
