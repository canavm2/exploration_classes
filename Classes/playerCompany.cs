﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Citizen;

namespace Company
{
    internal class PlayerCompany
    {
        #region Constructor
        public PlayerCompany(string name, CompanyMember master)
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
        public Dictionary<int, CompanyMember> companyMembers = new();
        public int MasterId { get; set; }
        public string MasterName { get; set; }

        #endregion

        #region Methods


        #endregion
    }
}
