using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using Company;
using Newtonsoft.Json;

namespace Relation
{
    //Object that holds the old relationships
    //these are relationships between citizens that are no longer in the same company
    public class RelationshipCache
    {
        public RelationshipCache()
        {
            OldRelationships = new();
        }
        [JsonConstructor]
        public RelationshipCache(Dictionary<string, Relationship> oldrelationships)
        {
            OldRelationships = oldrelationships;
        }
        public Dictionary<string, Relationship> OldRelationships { get; }

        public void CacheRelationship(Relationship relationship)
        {
            if (!OldRelationships.ContainsKey(relationship.Id))
                OldRelationships[relationship.Id] = relationship;
            else throw new Exception($"Relationship Cache already contains relationship: {relationship.Id}");
        }
        public Relationship RetrieveRelationship(string id)
        {
            Relationship retriviedRelationship = OldRelationships[id];
            OldRelationships.Remove(id);
            return retriviedRelationship;
        }
        public bool ContainsRelationships(string id)
        {
            return OldRelationships.ContainsKey(id);
        }
    }

    //Object that holds the various relationships between 2 citizens, only 1 object should exist, not one for each citizen
    public class Relationship
    {
        #region Constructors
        public Relationship(Citizen citizen1, Citizen citizen2)
        {
            Random random = new Random();
            Friendliness = random.Next(-10, 20);
            Teamwork = random.Next(-10, 20);
            Connection = 0;
            //Creates an ID with the citizen's IDs as: XXXX-YYYY, where XXXX is smaller than YYYY
            Id = CreateRelationshipId(citizen1.Id, citizen2.Id);
            if (citizen1.Id < citizen2.Id)
            {
                Citizen1Name = citizen1.Name;
                Citizen2Name = citizen2.Name;
            }
            else
            {
                Citizen1Name = citizen2.Name;
                Citizen2Name = citizen1.Name;
            }
        }

        [JsonConstructor]
        public Relationship(int friendliness, int teamwork, int connection, string id, string citizen1name, string citizen2name)
        {
            Friendliness = friendliness;
            Teamwork = teamwork;
            Connection = connection;
            Id = id;
            Citizen1Name = citizen1name;
            Citizen2Name = citizen2name;
        }
        #endregion

        #region Dictionaries and Properties
        public readonly string Id;
        public readonly string Citizen1Name;
        public readonly string Citizen2Name;
        public int Friendliness;
        public int Teamwork;
        public int Connection;
        #endregion

        #region Methods
        //Creates an ID with the citizen's IDs as: XXXX-YYYY, where XXXX is smaller than YYYY
        public static string CreateRelationshipId(int citId1, int citId2)
        {
            int Id1;
            int Id2;
            if (citId1 < citId2)
            {
                Id1 = citId1;
                Id2 = citId2;
            }
            else
            {
                Id1 = citId2;
                Id2 = citId1;
            }
            return $"{Id1}-{Id2}";
        }
        #endregion
    }
}
