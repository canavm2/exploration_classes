using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace exploration_classes
{
    internal class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, CompanyMember master)
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
        public Dictionary<int, CompanyMember> companyMembers = new Dictionary<int, CompanyMember>();
        public int masterId { get; set; }
        public string masterName { get; set; }

        #endregion

        #region Methods


        #endregion
    }
}
