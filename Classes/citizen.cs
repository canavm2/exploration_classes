using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploration_classes
{
    public class Citizen
    {
        #region Constructors
        // create citizen class which is the lowest level of a company member or NPC.  2 Constructors
        // first constructor creates a citizen with a specific Name, Age and Gender
        public Citizen(string name, int age, string gender)
        {
            Random random = new Random();
            Name = name;
            Age = age;
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";

            //Creates a "Random" ID
            // TODO: Fix, this is not perfect, will duplicate.
            Id = random.Next(10000,99999);
        }

        // second constructo allows the creation of random citizens
        public Citizen()
        {
            Random random = new Random();
            // TODO: Random Name Generator
            Name = "John Doe";
            Age = random.Next(12, 50);
            Gender = "male";

            //Creates a "Random" ID
            // TODO: Fix, this is not perfect, will duplicate.
            Id = random.Next(10000, 99999);
        }
        #endregion

        #region Descriptors and Stats
        private int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        private List<string> genders = new List<string>() { "male", "female", "non-binary" };

        public Stats stats = new Stats();
        #endregion



        #region Methods
        public void describeCitizen()
        {
            Console.WriteLine($"{Name}, a {Age} year old {Gender}.");
            Console.WriteLine();
            Console.WriteLine("His primary stats are:");
            Console.WriteLine($"Strength: {stats.primary["str"]}");
            Console.WriteLine($"Dexterity: {stats.primary["dex"]}");
            Console.WriteLine($"Intelligence: {stats.primary["smt"]}");
            Console.WriteLine($"Wisdom: {stats.primary["wis"]}");
            Console.WriteLine($"Charisma: {stats.primary["cha"]}");
            Console.WriteLine($"Leadership: {stats.primary["ldr"]}");
            Console.WriteLine();
            Console.WriteLine("His derived stats are:");
            Console.WriteLine($"Physical: {stats.derived["phys"]}");
            Console.WriteLine($"Mental: {stats.derived["mntl"]}");
            Console.WriteLine($"Social: {stats.derived["socl"]}");
            Console.WriteLine();
            Console.WriteLine($"This citizen's ID: {Id}");
        }
        public int getId()
        {
            return Id;
        }
        #endregion
    }
}
