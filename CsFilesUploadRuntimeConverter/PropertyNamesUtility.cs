using System;
using System.Collections.Generic;

namespace CsFilesUploadRuntimeConverter
{
    public static class PropertyNamesUtility
    {
        public static bool IsProperty(string line)
        {
            // even though I am avare that this is error prone I can't find better way to check if
            // given line is really variable
            return (line.Contains("private") ||
                    line.Contains("public"));
            // || (line.Contains("{ get"));
        }

        public static LineType GetPropertyType(string property, List<string> listOfClassNames, List<string> listOfVarTypes)
        {
            var retValue = new LineType
            {
                IsArray = false,
                PropertyType = PropertyType.Undefined
            };

            // Property types check
            foreach (var varType in listOfVarTypes)
            {
                if (property.ToLower().Contains(varType))
                {
                    retValue.PropertyType = PropertyType.PrimitiveType;
                    retValue.PropertyTypeName = varType.Trim();
                }
            }

            foreach (var className in listOfClassNames)
            {
                if (property.ToLower().Contains(className.ToLower()))
                {
                    retValue.PropertyType = PropertyType.ClassType;
                    retValue.PropertyTypeName = className.Trim();
                }
            }

            // Is property array?
            if (property.Contains("[") || property.ToLower().Contains("list"))
                retValue.IsArray = true;

            return retValue;
        }

        public static string StripPropertyName(string line, LineType pairLineType)
        {
            if (pairLineType.PropertyType != PropertyType.Undefined)
            {
                int startIndex;

                // Code below was working just fine untill damn DateTime happened, now I don't even know what is happening
                // var startIndex = line.IndexOf(pairLineType.PropertyTypeName, StringComparison.Ordinal) + pairLineType.PropertyTypeName.Length + 1;
                if (pairLineType.IsArray)
                {
                    startIndex = line.IndexOf(pairLineType.PropertyTypeName, StringComparison.Ordinal) + pairLineType.PropertyTypeName.Length + 1;
                }
                else
                {
                    startIndex = line.ToLower().IndexOf(pairLineType.PropertyTypeName.ToLower(), StringComparison.Ordinal);
                    startIndex = startIndex + pairLineType.PropertyTypeName.Length + 1;
                }
                int endIndex;

                var hasCurly = line.Contains("{");
                if (!hasCurly)
                {
                    endIndex = line.LastIndexOf(";", StringComparison.Ordinal);
                }
                else
                {
                    endIndex = line.LastIndexOf("{", StringComparison.Ordinal);
                }

                if (endIndex <= startIndex)
                    return line.Substring(startIndex);

                endIndex = endIndex - startIndex;

                // For some reason I have to do this for property vals
                if (hasCurly)
                {
                    endIndex = endIndex - 1;
                }

                return line.Substring(startIndex, endIndex).Trim();
            }
            else
            {
                // if type is undefined, just f**k sh*t up
                var retS = line
                    .Replace("{", "")
                    .Replace("}", "")
                    .Replace("get", "")
                    .Replace("set", "")
                    .Replace(";", "")
                    .Replace("Int", "")
                    .Replace("Int16", "")
                    .Replace("Int32", "")
                    .Replace("Int64", "")
                    .Replace("UInt", "")
                    .Replace("Short", "")
                    .Replace("Bool", "")
                    .Replace("Boolean", "")
                    .Replace("Byte", "")
                    .Replace("SByte", "")
                    .Replace("Char", "")
                    .Replace("Date", "")
                    .Replace("DateTime", "")
                    .Replace("Decimal", "")
                    .Replace("Double", "")
                    .Replace("Float", "")
                    .Replace("String", "")
                    .Replace("Object", "")
                    .Replace("public", "")
                    .Replace("private", "")
                    .Trim();

                return retS;
            }
        }
    }
}
