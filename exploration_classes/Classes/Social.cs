using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Relation;

namespace People
{
    public class Social
    {
        #region Constructors
        public Social()
        {
            Relationships = new();
        }

        [JsonConstructor]
        public Social(Dictionary<string, Relationship> relationships)
        {
            Relationships = relationships;
        }
        #endregion

        #region Dictionaries and Properties
        //Stores the relationships in a dictionary using the other citizen's ID as the key
        //value is a subclass called relationship, which is an object that has 3 parameters
        //one for each type of relationship.
        public Dictionary<string, Relationship> Relationships;
        #endregion

        #region Methods
        #endregion


    }
}
