using System;

namespace TopDecks.Api.Core.Extensions
{
    public static class StringExtensions
    {
        public static string ToLowerCaseFirstLetter(this string s)
        {
            return char.ToLower(s[0]) + s.Substring(1);
        }

        public static string CapitalizeIfUppercase(this string s)
        {
            return s.Length > 1 && s == s.ToUpper() ? s.Capitalize() : s;
        }
        public static string Replace(this string originalString, string oldValue, string newValue, StringComparison comparisonType)
        {
            var startIndex = 0;
            while (true)
            {
                startIndex = originalString.IndexOf(oldValue, startIndex, comparisonType);
                if (startIndex == -1)
                    break;

                originalString = originalString.Substring(0, startIndex) + newValue + originalString.Substring(startIndex + oldValue.Length);

                startIndex += newValue.Length;
            }

            return originalString;
        }
    }
}