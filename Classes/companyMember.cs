using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Citizen
{
    public class CompanyMember : Citizen
    {
        public CompanyMember(string name, string gender, int age = 0) : base(name, gender, age)
        {

        }
    }
}