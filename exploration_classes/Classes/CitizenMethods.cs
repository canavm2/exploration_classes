using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public partial class Citizen
    {
        public string DescribeCitizen()
        {
            string returnDescription =
                $"\n{Name}, a {Age} year old {Gender}.\n" +
                $"\nTheir stats are:\n\n" +
                DescribeStats() +
                $"\nTheir skills are:\n\n" +
                Skills.Describe() +
                $"\nThis citizen's ID: {Id}\n\n";

            return returnDescription;
        }

        public string DescribeStats()
        {
            //Iterates over all the Primary stats, and provides a string that describes it
            string primaryDesc = "";
            foreach (KeyValuePair<string, Stat> stat in PrimaryStats)
            {
                string tempDesc = $"{stat.Key.ToUpper()}: {stat.Value.Full.ToString()}\n";
                primaryDesc += tempDesc;
            }
            string derivedDesc = "";
            foreach (KeyValuePair<string, Stat> stat in DerivedStats)
            {
                string tempDesc = $"{stat.Key.ToUpper()}: {stat.Value.Full.ToString()}\n";
                derivedDesc += tempDesc;
            }
            string description =
                $"Primary Stats:\n" +
                primaryDesc +
                $"\nDerived Stats:\n" +
                derivedDesc;
            ;
            return description;
        }
    }
}
