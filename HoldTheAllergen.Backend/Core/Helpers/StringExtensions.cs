using System.Text.RegularExpressions;

namespace HoldTheAllergen.Backend.Core.Helpers
{
    public static class StringExtensions
    {
        public static string ToSeparatedWords(this string value)
        {
            return value != null ? Regex.Replace(value, "([A-Z][a-z]?)", " $1").Trim() : null;
        }

        public static string LimitTo(this string value, int numberOfCharactersToReturn)
        {
            if (value == null) return null;
            return value.Length <= numberOfCharactersToReturn
                       ? value
                       : string.Format("{0} ...", value.Substring(0, numberOfCharactersToReturn));
        }

        public static string ToUrlSlug(this string value, int maxLength = 50)
        {
            var str = value.ToLower();

            // invalid chars, make into spaces
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces/hyphens into one space       
            str = Regex.Replace(str, @"[\s-]+", " ").Trim();
            // cut and trim it
            str = str.Substring(0, str.Length <= maxLength ? str.Length : maxLength).Trim();
            // hyphens
            str = Regex.Replace(str, @"\s", "-");

            return str;
        }
    }
}