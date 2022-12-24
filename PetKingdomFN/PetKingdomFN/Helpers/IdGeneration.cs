using System.Text.RegularExpressions;

namespace PetKingdomFN.Helpers
{
    public class IdGeneration
    {
        public  async Task<string> generateId(string previousId)
        {
            var prefix = Regex.Match(previousId, "^\\D+").Value;
            var number = Regex.Replace(previousId, "^\\D+", "");
            var i = int.Parse(number) + 1;
            var newId = prefix + i.ToString();
            return newId;
        }
    }
}
