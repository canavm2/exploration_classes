using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using People;
using Company;
using Newtonsoft.Json;

namespace Relationships
{
    //Object that holds the old relationships
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
        public Dictionary<string, Relationship> OldRelationships;
    }
    public class Relationships
    {
        //Takes two Citizen IDs and returns them as XXXX-YYYY where XXXX<YYYY
        //so they can be used as a relationship key
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

        //Replaces the citizen provided with the citizen currently in the provided role.  The replaced citizen is stored in the citizen vault.
        public static void ReplaceAdvisor(Citizen citizen, PlayerCompany playercompany, string role, CitizenCache citizencache)
        {
            if (playercompany.Advisors[role] == null) throw new ArgumentNullException($"No citizen to replace in role: {role}.");
            Citizen replacedCitizen = playercompany.Advisors[role];
            playercompany.Advisors[role] = citizen;

        }
    }
    public class Relationship
    {
        public Relationship(Citizen citizen1, Citizen citizen2)
        {
            Random random = new Random();
            Friendliness = random.Next(-10, 20);
            Teamwork = random.Next(-10, 20);
            Connection = 0;
            Id = Relationships.CreateRelationshipId(citizen1.Id, citizen2.Id);
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

        public readonly string Id;
        public readonly string Citizen1Name;
        public readonly string Citizen2Name;
        public int Friendliness;
        public int Teamwork;
        public int Connection;

    }
}
