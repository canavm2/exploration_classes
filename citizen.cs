using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploration_classes
{
    public class Citizen
    {
        #region RandomNumberGenerator
        // create a "random" object which can be used to create random numbers
        // TODO: Can this be moved out of the class?
        private Random random = new Random();
        #endregion

        #region Constructors
        // create citizen class which is the lowest level of a company member or NPC.  2 Constructors
        // first constructor creeates a citizen with a specific Name, Age and Gender
        public Citizen(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";
            randomStats();

            //Creates a "Random" ID
            // TODO: Fix, this is not perfect, will duplicate.
            Id = random.Next(10000,99999);
        }

        // second constructo allows the creation of random citizens
        public Citizen()
        {
            // TODO: Random Name Generator
            Name = "John Doe";
            Age = random.Next(12, 50);
            Gender = "male";
            randomStats();

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

        public int str { get; set; }
        public int dex { get; set; }
        public int smt { get; set; }
        public int wis { get; set; }
        public int cha { get; set; }
        public int ldr { get; set; }
        public int phys { get; set; }
        public int mntl { get; set; }
        public int socl { get; set; }

        // Generate random primary stats for the citizen
        public void randomStats()
        {
            str = random.Next(10, 30);
            dex = random.Next(10, 30);
            smt = random.Next(10, 30);
            wis = random.Next(10, 30);
            cha = random.Next(10, 30);
            ldr = random.Next(10, 30);
            calculateDerStats();
        }

        // calculate the derived stats based on the primary stats.
        public void calculateDerStats()
        {
            phys = str + dex;
            mntl = smt + wis;
            socl = cha + ldr;
        }

        // takes a list of integers that must be ordered like the stats and assigns them
        public void assignStats(List<int> stats)
        {
            str = stats[0];
            dex = stats[1];
            smt = stats[2];
            wis = stats[3];
            cha = stats[4];
            ldr = stats[5];
            calculateDerStats();
        }
        #endregion

        #region Methods
        public void describeCitizen()
        {
            Console.WriteLine($"{Name}, a {Age} year old {Gender}.");
            Console.WriteLine();
            Console.WriteLine("His primary stats are:");
            Console.WriteLine($"Strength: {str}");
            Console.WriteLine($"Dexterity: {dex}");
            Console.WriteLine($"Intelligence: {smt}");
            Console.WriteLine($"Wisdom: {wis}");
            Console.WriteLine($"Charisma: {cha}");
            Console.WriteLine($"Leadership: {ldr}");
            Console.WriteLine();
            Console.WriteLine("His derived stats are:");
            Console.WriteLine($"Physical: {phys}");
            Console.WriteLine($"Mental: {mntl}");
            Console.WriteLine($"Social: {socl}");
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
