using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace People
{
    public class Modifier
    {
        #region Constructors
        // IMPORTANT: Json Deserialization uses the name of the property as the parameter
        // if the property is readonly it must match or it will not be able to change it after the constructor
        public Modifier(string name, string source, string type, string modifiedvalue, int value, bool temporary, int duration, string description)
        {
            List<string> possibletypes = new List<string>() {"skill", "stat", "attribute"};
            if (!possibletypes.Contains(type)) throw new Exception($"Modifier type not found: {type}.");
            Name = name;
            Description = description;
            Source = source;
            Type = type;
            ModifiedValue = modifiedvalue;
            Value = value;
            Temporary = temporary;
            Duration = duration;
            //TODO change exception
            if (duration < 0) throw new Exception($"Negative Duration: {duration}");
            Id = name + "-" + source;
        }
        #endregion

        #region Properties
        public readonly string Name;
        public readonly string Description;
        public readonly string Source;
        public readonly string Type;
        public readonly string ModifiedValue;
        public readonly int Value;
        public readonly bool Temporary;
        public readonly int Duration;
        public readonly string Id;
        #endregion

        #region Methods
        public string Summary()
        {
            string returnSummary = $"Citizen Stat Modifier: {Name}\n" +
                $"{ModifiedValue}: {Value}\n" +
                $"Description: {Description}\n" +
                $"ID: {Id}";
            if (Temporary)
                returnSummary = returnSummary + $"\n" +
                    $"Duration: {Duration}\n";
            return returnSummary;
        }
        #endregion
    }
}
