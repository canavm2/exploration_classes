using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizen
{
    public class Citizen
    {
        #region Constructors
        // create citizen class which is the lowest level of a company member or NPC.  
        // constructor requires a name, gender and optional age
        // random names can be passed 
        public Citizen(string name, string gender, int age = 0)
        {
            Random random = new();
            if (age == 0)
                age = random.Next(15,40);
            Name = name;
            Age = age;
            List<string> genders = new() { "male", "female", "non-binary" };
            if (genders.Contains(gender))
                Gender = gender;
            else
                Gender = "non-binary";

            //Creates a "Random" ID
            // TODO: Fix, this is not perfect, will duplicate.
            Id = random.Next(10000,99999);
        }

        //public Citizen()
        //{
        //    Name = "test";
        //    Gender = "test";
        //}
        #endregion

        #region Descriptors and Stats
        private int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }

        public CitizenStats stats = new();
        #endregion



        #region Methods
        public void DescribeCitizen()
        {
            Console.WriteLine($"{Name}, a {Age} year old {Gender}.");
            Console.WriteLine();
            Console.WriteLine("Their primary stats are:");
            Console.WriteLine($"Strength: {stats.primary["str"]}");
            Console.WriteLine($"Dexterity: {stats.primary["dex"]}");
            Console.WriteLine($"Intelligence: {stats.primary["smt"]}");
            Console.WriteLine($"Wisdom: {stats.primary["wis"]}");
            Console.WriteLine($"Charisma: {stats.primary["cha"]}");
            Console.WriteLine($"Leadership: {stats.primary["ldr"]}");
            Console.WriteLine();
            Console.WriteLine("Their derived stats are:");
            Console.WriteLine($"Physical: {stats.derived["phys"]}");
            Console.WriteLine($"Mental: {stats.derived["mntl"]}");
            Console.WriteLine($"Social: {stats.derived["socl"]}");
            Console.WriteLine();
            Console.WriteLine($"This citizen's ID: {Id}");
        }
        public int GetId()
        {
            return Id;
        }
        #endregion
    }
}
