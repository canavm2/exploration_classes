using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizen
{
    public class CitizenStats
    {
        public CitizenStats()
        {
            Random random = new Random();
            foreach (string pstat in primaryStats)
            {
                primary[pstat] = random.Next(10,30);
            }
            derived["phys"] = (primary["str"] + primary["dex"])/2;
            derived["mntl"] = (primary["smt"] + primary["wis"])/ 2;
            derived["socl"] = (primary["cha"] + primary["ldr"])/ 2;
        }

        public List<string> primaryStats = new List<string>()
        {
            "str","dex","smt","wis","cha","ldr"
        };
        public List<string> derivedStats = new List<string>()
        {
            "phys","mntl","socl"
        };
        public Dictionary<string, int> primary = new Dictionary<string, int>();
        public Dictionary<string, int> derived = new Dictionary<string, int>();
    }
}
