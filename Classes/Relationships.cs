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
        public static void ReplaceAdvisor(Citizen citizen, PlayerCompany playercompany, string role, CitizenCache citizencache, RelationshipCache relationshipcache)
        {
            if (playercompany.Advisors[role] == null) throw new ArgumentNullException($"No citizen to replace in role: {role}.");
            Citizen replacedCitizen = playercompany.Advisors[role];
            playercompany.Advisors[role] = citizen;
            UpdateRelationships(playercompany, relationshipcache);
            citizencache.CacheCitizen(replacedCitizen);
        }

        //Long Important Method
        //Updates the relationships
        //finds relationships that don't need to be stored on the playercompany and moves them to the relationshipcache
        //looks for any missing relationships and fixes the gaps:
        //takes missing relationships from the relationship cache and creates new ones for those that dont exist
        public static void UpdateRelationships(PlayerCompany playercompany, RelationshipCache relationshipcache)
        {
            List<int> advisorIds = new();
            int relationshipCount = 0;
            int oldrelationships = 0;
            int newrelationships = 0;
            foreach (Citizen advisor in playercompany.Advisors.Values)
            {
                advisorIds.Add(advisor.Id);
            }
            //iterates through each relationship in Social
            foreach (KeyValuePair<string, Relationship> kvp in playercompany.Social.Relationships)
            {
                string key = kvp.Key;
                string[] ids = key.Split("-");
                int id1 = int.Parse(ids[0]);
                int id2 = int.Parse(ids[1]);
                //check to see if the key contains ids from two current advisors
                //if it doesnt match 2 current advisors, it removes it and stores it in the relationshipcache
                if (advisorIds.Contains(id1) && advisorIds.Contains(id2))
                {
                    relationshipCount++;
                }
                else
                {
                    oldrelationships++;
                    relationshipcache.CacheRelationship(kvp.Value);
                    playercompany.Social.Relationships.Remove(kvp.Key);
                }
            }
            //Iterates through each pair of advisors in the advisor list
            foreach (int id in advisorIds)
            {
                foreach (int id2 in advisorIds)
                {
                    //skips looking for themselves
                    if (id != id2)
                    {
                        string relationshipId = CreateRelationshipId(id, id2);
                        //Checks to see if a relationship already exists in the company
                        if (!playercompany.Social.Relationships.ContainsKey(relationshipId))
                        {
                            //checks to see if the relationship exists in the cache
                            if (relationshipcache.ContainsRelationships(relationshipId))
                            {
                                //retrieves the relationship from the cache if it exists there
                                playercompany.Social.Relationships.Add(relationshipId,relationshipcache.RetrieveRelationship(relationshipId));
                            }
                            //creates a new relationship if it doesnt exist anywhere
                            else
                            {
                                //turns the Ids back into citizens, uses lambda functions to search the advisors dictionary by a property instead of a key
                                //creates and then adds the new relationship to the playercompany
                                Citizen citizen1 = playercompany.Advisors.Where(x => x.Value.Id == id).FirstOrDefault().Value;
                                Citizen citizen2 = playercompany.Advisors.Where(x => x.Value.Id == id2).FirstOrDefault().Value;
                                Relationship newrelationship = new(citizen1, citizen2);
                                playercompany.Social.Relationships.Add(relationshipId,newrelationship);
                            }
                            newrelationships++;
                        }
                    }
                }
            }
            //TODO remove this tracking
            Console.WriteLine($"There are {relationshipCount} good relationships, and {oldrelationships} old relationships removed, with {newrelationships} new relationships added.");
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
