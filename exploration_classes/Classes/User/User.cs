using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Company;
using People;

namespace Users
{
    public class User
    {
        #region Constructors
        public User(string userName)
        {
            UserName = userName;
            id = new Guid();
        }

        [JsonConstructor]
        public User(string userName, Guid Id, Guid companyId)
        {
            UserName = userName;
            id = Id;
            CompanyId = companyId;
        }
        #endregion

        #region Dictionaries and Properties
        public string UserName { get; set; }
        public Guid id { get; set; }
        public Guid CompanyId { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
