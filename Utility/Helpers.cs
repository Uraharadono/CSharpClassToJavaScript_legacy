using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace Utility
{
    public static class Helpers
    {
        public static string GetName(string input, List<string> nameFilters)
        {
            return nameFilters == null
                ? input
                : nameFilters.Aggregate(input, (current, nameFilter) => current.Replace(nameFilter, string.Empty));
        }

        public static string ToCamelCase(string input, bool camelCase)
        {
            if (!camelCase) return input;

            var s = input;
            if (!char.IsUpper(s[0])) return s;

            var cArr = s.ToCharArray();
            for (var i = 0; i < cArr.Length; i++)
            {
                if (i > 0 && i + 1 < cArr.Length && !char.IsUpper(cArr[i + 1])) break;
                cArr[i] = char.ToLowerInvariant(cArr[i]);
            }
            return new string(cArr);
        }

        public static bool IsCollectionType(Type propertyType)
        {

            return (propertyType.GetInterfaces().Contains(typeof(IList)) ||
                    propertyType.GetInterfaces().Contains(typeof(ICollection)) ||
                    propertyType.GetInterfaces().Contains(typeof(IDictionary)) ||
                    propertyType.IsArray);
        }

        public static bool IsDictionaryType(Type propertyType)
        {
            return (propertyType.GetInterfaces().Contains(typeof(IDictionary)));
        }

        public static bool IsPrimitive(Type propertyType)
        {
            return propertyType.IsPrimitive || propertyType.IsValueType ||
                   propertyType == typeof(string);
        }

        public static bool ShouldGenerateMember(PropertyInfo propertyInfo, JsGeneratorOptions generatorOptions)
        {
            if (!generatorOptions.RespectDataMemberAttribute) return true;

            var customAttributes = propertyInfo.GetCustomAttributes(true);

            return customAttributes.All(p => (p as IgnoreDataMemberAttribute) == null);
        }

        public static bool HasDefaultValue(PropertyInfo propertyInfo, JsGeneratorOptions generatorOptions)
        {
            if (!generatorOptions.RespectDefaultValueAttribute) return false;

            var customAttributes = propertyInfo.GetCustomAttributes(true);

            if (customAttributes.All(p => (p as DefaultValueAttribute) == null)) return false;

            return true;
        }

        public static object ReadDefaultValueFromAttribute(PropertyInfo propertyInfo)
        {
            var customAttributes = propertyInfo.GetCustomAttributes(true);

            var defaultValueAttribute = (DefaultValueAttribute)customAttributes.First(p => (p as DefaultValueAttribute) != null);

            return defaultValueAttribute.Value;
        }

        public static string GetPropertyName(PropertyInfo propertyInfo, JsGeneratorOptions generatorOptions)
        {
            if (!generatorOptions.RespectDataMemberAttribute) return propertyInfo.Name;

            var customAttributes = propertyInfo.GetCustomAttributes(true);

            if (customAttributes.All(p => (p as DataMemberAttribute) == null)) return propertyInfo.Name;

            var dataMemberAttribute = (DataMemberAttribute)customAttributes.First(p => (p as DataMemberAttribute) != null);

            return !string.IsNullOrWhiteSpace(dataMemberAttribute.Name) ? dataMemberAttribute.Name : propertyInfo.Name;
        }
    }
}
