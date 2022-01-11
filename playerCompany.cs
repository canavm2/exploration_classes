using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploration_classes
{
    internal class playerCompany
    {
        #region Constructor
        public playerCompany(string name, companyMember master)
        {
            Name = name;
            masterId = master.getId();
            masterName = master.Name;
            companyMembers[masterId] = master;
        }
        #endregion

        #region Company Descriptors
        public string Name { get; set; }
        public int Id { get; set; }
        public Dictionary<int, companyMember> companyMembers = new Dictionary<int, companyMember>();
        public int masterId { get; set; }
        public string masterName { get; set; }

        #endregion
    }
}
