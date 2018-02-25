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
                    retValue.PropertyTypeName = varType;
                }
            }
            foreach (var className in listOfClassNames)
            {
                if (property.ToLower().Contains(className))
                {
                    retValue.PropertyType = PropertyType.ClassType;
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
                var startIndex = line.IndexOf(pairLineType.PropertyTypeName, StringComparison.Ordinal) + pairLineType.PropertyTypeName.Length + 1;
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

                return line.Substring(startIndex, endIndex);
            }
            else
            {
                // TODO: 
                return "";
            }
        }
    }
}
