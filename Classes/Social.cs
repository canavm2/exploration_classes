using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Relationships;

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
        ////Takes two Citizen IDs and returns them as XXXX-YYYY where XXXX<YYYY
        ////so they can be used as a relationship key
        //public static string CreateRelationshipId(int citId1, int citId2)
        //{
        //    int Id1;
        //    int Id2;
        //    if (citId1 < citId2)
        //    {
        //        Id1 = citId1;
        //        Id2 = citId2;
        //    }
        //    else
        //    {
        //        Id1 = citId2;
        //        Id2 = citId1;
        //    }
        //    return $"{Id1}-{Id2}";
        //}
        #endregion

        #region Subclasses
        //public class Relationship
        //{
        //    public Relationship(Citizen citizen1, Citizen citizen2)
        //    {
        //        Random random = new Random();
        //        Friendliness = random.Next(-10, 20);
        //        Teamwork = random.Next(-10, 20);
        //        Connection = 0;
        //        Id = CreateRelationshipId(citizen1.Id, citizen2.Id);
        //        if (citizen1.Id < citizen2.Id)
        //        {
        //            Citizen1Name = citizen1.Name;
        //            Citizen2Name = citizen2.Name;
        //        }
        //        else
        //        {
        //            Citizen1Name = citizen2.Name;
        //            Citizen2Name = citizen1.Name;
        //        }
        //    }

        //    [JsonConstructor]
        //    public Relationship(int friendliness, int teamwork, int connection, string id, string citizen1name, string citizen2name)
        //    {
        //        Friendliness = friendliness;
        //        Teamwork = teamwork;
        //        Connection = connection;
        //        Id = id;
        //        Citizen1Name = citizen1name;
        //        Citizen2Name = citizen2name;
        //    }

        //    public readonly string Id;
        //    public readonly string Citizen1Name;
        //    public readonly string Citizen2Name;
        //    public int Friendliness;
        //    public int Teamwork;
        //    public int Connection;

        //}
        
        #endregion

        //public string Describe()
        //{
        //    //Iterates over all the Skills, and provides a string that describes it
        //    string vocDesc = "";
        //    foreach (KeyValuePair<string, int> skill in VocSkill)
        //    {
        //        string tempDesc = $"{skill.Key}: {skill.Value.ToString()}\n";
        //        vocDesc += tempDesc;
        //    }
        //    string expDesc = "";
        //    foreach (KeyValuePair<string, int> skill in ExpSkill)
        //    {
        //        string tempDesc = $"{skill.Key}: {skill.Value.ToString()}\n";
        //        expDesc += tempDesc;
        //    }
        //    string description =
        //        $"Vocational Skills:\n" +
        //        vocDesc +
        //        $"\nExperiential Skills:\n" +
        //        expDesc;
        //    return description;
        //}

    }
}
