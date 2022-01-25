using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace People
{
    //public class Attributes
    //{
    //    #region Constructors
    //    public Attributes()
    //    {
    //        List<string> attributes = new List<string>() {
    //            "Health",
    //            "Happiness",
    //            "Motivation",
    //            "Psyche"
    //        };
    //        CitizenAttributes = new();
    //        Modifiers = new();
    //        foreach (string attribute in attributes)
    //            CitizenAttributes.Add(attribute, new Attribute());
    //    }

    //    [JsonConstructor]
    //    public Attributes(Dictionary<string, Attribute> citizenattributes, List<Modifier> modifiers)
    //    {
    //        CitizenAttributes = citizenattributes;
    //        Modifiers = modifiers;
    //    }
    //    #endregion

    //    #region Dictionaries and Properties
    //    public Dictionary<string, Attribute> CitizenAttributes;
    //    public List<Modifier> Modifiers;

    //    #endregion


    //    #region Subclasses
    //    public class Attribute
    //    {
    //        #region Constructors
    //        public Attribute()
    //        {
    //            Full = 10;
    //            Unmodified = 10;
    //        }

    //        [JsonConstructor]
    //        public Attribute(int full, int unmodified)
    //        {
    //            Full=full;
    //            Unmodified=unmodified;
    //        }
    //        #endregion
    //        public int Full;
    //        public int Unmodified;
    //    }

    //    #endregion
    //}



}
