namespace APIMethods;
using People;
using Company;
using Users;
using Relation;
using FileTools;

public class APICalls
{
    public static string ReturnTest(string returnString)
    {
        return returnString;
    }
    public static string ReturnCitizen(CitizenCache cache)
    {
        Citizen citizen = cache.GetRandomCitizen();
        return citizen.DescribeCitizen();
    }
    public static string ReturnCitizenFromCompany(PlayerCompany playercompany, int id)
    {
        if (id == 0) return playercompany.Advisors["master"].DescribeCitizen();
        else if (id > 0 && id < 6)
        {
            string advisorkey = $"advisor{id}";
            return playercompany.Advisors[advisorkey].DescribeCitizen();
        }
        else if (id >= 6 && id < playercompany.Advisors.Count)
        {
            string advisorkey = $"bench{id-6}";
            return playercompany.Advisors[advisorkey].DescribeCitizen();
        }
        else return $"Error: id must be between 0 and {playercompany.Advisors.Count}";
    }
    public static async Task<string> Save(FileTool fileTool, CitizenCache citizenCache, UserCache userCache, CompanyCache companyCache, RelationshipCache relationshipCache)
    {
        await fileTool.StoreCitizens(citizenCache);
        await fileTool.StoreCompanies(companyCache);
        await fileTool.StoreRelationshipCache(relationshipCache);
        await fileTool.StoreUsers(userCache);
        return "Everything saved!  Beep Beep Woop Woop";
    }
    public static string CreateUser(string userName, UserCache userCache, CitizenCache citizenCache, CompanyCache companyCache)
    {
        if (userCache.Users.ContainsKey(userName)) return "UserName already Exists, choose something else.";
        userCache.CreateNewUser(userName, citizenCache, companyCache);
        return "User Created, use your UserID in the API now.";
    }
}
