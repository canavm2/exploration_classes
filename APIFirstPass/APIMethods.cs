namespace APIMethods;
using People;
using Company;

public class CitizenDB
{
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
}
