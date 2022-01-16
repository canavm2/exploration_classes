using Newtonsoft.Json;
using People;

namespace FileTools
{
    public class FileTool
    {
        public static void StoreCitizens(List<Citizen> citizens, string filename)
        {
            string jsoncitizen = JsonConvert.SerializeObject(citizens);
            File.WriteAllText($"../../../{filename}.txt", jsoncitizen.ToString());
        }
        public static List<Citizen> ReadCitizens(string filename)
        {
            string fileJson = File.ReadAllText($"../../../{filename}.txt");
            List <Citizen> citizens = new List <Citizen>();
            citizens = JsonConvert.DeserializeObject<List<Citizen>>(fileJson);
            return citizens;
        }
    }
}
