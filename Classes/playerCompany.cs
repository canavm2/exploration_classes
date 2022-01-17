using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;

namespace Company
{
    internal class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, Citizen master)
        {
            Name = name;
            MasterId = master.GetId();
            MasterName = master.Name;
            companyMembers[MasterId] = master;
        }
        #endregion

        #region Company Descriptors
        public string Name { get; set; }
        public int Id { get; set; }
        public Dictionary<int, Citizen> companyMembers = new();
        public int MasterId { get; set; }
        public string MasterName { get; set; }

        #endregion

        #region Methods


        #endregion
    }
}
