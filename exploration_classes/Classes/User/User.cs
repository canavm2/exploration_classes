using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Company;
using People;

namespace User
{
    public class User
    {
        #region Constructors
        public User(string userName, CitizenCache citizenCache)
        {
            UserName = userName;
            id = new Guid();
        }

        [JsonConstructor]
        public User(string userName, Guid Id)
        {
            UserName = userName;
            id = Id;
        }
        #endregion

        #region Dictionaries and Properties
        public string UserName { get; set; }
        public Guid id { get; set; }
        #endregion

        #region Methods
        #endregion
    }
}
