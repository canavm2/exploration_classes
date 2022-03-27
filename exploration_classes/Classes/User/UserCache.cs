using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace User
{
    public class UserCache
    {
        #region Constructors
        public UserCache()
        {
            id = Guid.NewGuid();
            Users = new();
        }

        [JsonConstructor]
        public UserCache(Guid Id, Dictionary<string, User> users)
        {
            id = Id;
            Users = users;
        }
        #endregion

        #region Dictionaries and Properties
        public Guid id { get; set; }
        public Dictionary<string, User> Users { get; set; }
        #endregion

        #region Methods
        #endregion

        #region Subclasses
        #endregion
    }
}
