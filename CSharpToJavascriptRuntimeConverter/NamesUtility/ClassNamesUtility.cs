using System;

namespace CSharpToJavascriptRuntimeConverter.NamesUtility
{
    public static class ClassNamesUtility
    {
        public static bool IsClass(string line)
        {
            return line.Contains("class");
        }

        public static string StripClassName(string line)
        {
            var startIndex = line.IndexOf("class ", StringComparison.Ordinal) + 6;
            int endIndex;

            var hasCurly = line.Contains("{");
            if (!hasCurly)
            {
                endIndex = line.LastIndexOf(" ", StringComparison.Ordinal);
            }
            else
            {
                endIndex = line.LastIndexOf("{", StringComparison.Ordinal);
            }
            endIndex =  endIndex - startIndex - 1;

            if (endIndex <= startIndex)
                return line.Substring(startIndex);

            return line.Substring(startIndex, endIndex);
        }
    }
}
