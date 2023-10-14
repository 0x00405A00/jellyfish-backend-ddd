using System.Text.Json;
using System.Text.RegularExpressions;

namespace Shared.Json
{
    public class LowerCaseNamingPolicy : JsonNamingPolicy
    {
        public static Regex InsertBeforeCamelCaseLetterUnderscore = new Regex(@"(\p{Lu})");
        public override string ConvertName(string name)
        {
            if (String.IsNullOrEmpty(name))
                return name;

            var newName = InsertBeforeCamelCaseLetterUnderscore.Replace(name, "_$1");
            if(newName.StartsWith("_"))
            {
                newName = newName.Substring(1);
            }
            return newName.ToLower();
        }
    }
}
