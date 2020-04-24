using System.Text.RegularExpressions;

namespace Common
{
    public static class Validator
    {
        public static bool ValidateIntegersFromString(string _string)
        {
            string pattern = @"(^[0-9]+$)";
            Regex regex = new Regex(pattern);

            return regex.IsMatch(_string);
        }
    }
}
