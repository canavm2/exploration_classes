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
        return citizen.Describe();
    }
    public static string ReturnCitizenFromCompany(PlayerCompany playercompany, int id)
    {
        if (id == 0) return playercompany.Advisors["master"].Describe();
        else if (id > 0 && id < 6)
        {
            string advisorkey = $"advisor{id}";
            return playercompany.Advisors[advisorkey].Describe();
        }
        else if (id >= 6 && id < playercompany.Advisors.Count)
        {
            string advisorkey = $"bench{id-6}";
            return playercompany.Advisors[advisorkey].Describe();
        }
        else return $"Error: id must be between 0 and {playercompany.Advisors.Count}";
    }
    public static async Task<string> AdvanceSave(FileTool fileTool, CitizenCache citizenCache, UserCache userCache, CompanyCache companyCache, RelationshipCache relationshipCache)
    {
        DateTime currentDateTime = DateTime.Now;
        TimeSpan timeSpan = currentDateTime - userCache.LastSave;
        int interval = Convert.ToInt32(timeSpan.TotalSeconds);
        foreach (User user in userCache.Users.Values)
        {
            user.GainTimePoints(interval);
        }
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
