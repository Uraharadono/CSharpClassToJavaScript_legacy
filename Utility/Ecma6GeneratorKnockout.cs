using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class Ecma6KnockoutGenerator
    {
        public static JsGeneratorOptions Options { get; set; } = new JsGeneratorOptions();

        private static readonly List<Type> AllowedDictionaryKeyTypes = new List<Type>()
        {
            typeof(int),
            typeof(string),
            typeof(Enum)
        }; 

        public static string Generate(IEnumerable<Type> typesToGenerate, JsGeneratorOptions generatorOptions = null)
        {
            var passedOptions = generatorOptions ?? Options;
            if (passedOptions == null)
            {
                throw new ArgumentNullException(nameof(passedOptions), "Options cannot be null.");
            }
            var propertyClassCollection = TypePropertyDictionaryGenerator.GetPropertyDictionaryForTypeGeneration(typesToGenerate, passedOptions);
            var js = GenerateJs(propertyClassCollection, passedOptions);
            return js;
        }

        private static string GenerateJs(IEnumerable<PropertyBag> propertyCollection, JsGeneratorOptions generationOptions)
        {
            var options = generationOptions;
            
            var sbOut = new StringBuilder();

            foreach (var type in propertyCollection.GroupBy(r => r.TypeName))
            {
                var sb = new StringBuilder();

                BuildClassConstructor(type, sb, options);

                var propList = type.GroupBy(t => t.PropertyName).Select(t => t.First()).ToList();
                foreach (var propEntry in propList)
                {
                    switch (propEntry.TransformablePropertyType)
                    {
                        case PropertyBag.TransformablePropertyTypeEnum.CollectionType:
                            BuildArrayProperty(sb, propEntry, options);
                            break;
                        case PropertyBag.TransformablePropertyTypeEnum.DictionaryType:
                            BuildDictionaryProperty(sb, propEntry, options);
                            break;
                        case PropertyBag.TransformablePropertyTypeEnum.ReferenceType:
                            BuildObjectProperty(sb, propEntry, options);
                            break;
                        case PropertyBag.TransformablePropertyTypeEnum.Primitive:
                            BuildPrimitiveProperty(propEntry, sb, options);
                            break;
                    }
                }

                if (options.CustomFunctionProcessors?.Any() == true)
                {
                    foreach (var customProcessor in options.CustomFunctionProcessors)
                    {
                        sb.AppendLine();
                        customProcessor(sb, propList, options);
                    }
                }
                // BuildClassClosure(sb);
                BuildClassClosure(sb, type, options);

                sbOut.AppendLine(sb.ToString());
                sbOut.AppendLine();
            }

            return sbOut.ToString();
        }

        private static void BuildClassClosure(StringBuilder sb, IGrouping<string, PropertyBag> type, JsGeneratorOptions options)
        {
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine($"export default { Helpers.GetName(type.First().TypeName, options.ClassNameConstantsToRemove)};");
        }

        private static void BuildClassClosure(StringBuilder sb)
        {
            sb.AppendLine("    }");
            sb.AppendLine("}");
        }

        private static void BuildClassConstructor(IGrouping<string, PropertyBag> type, StringBuilder sb, JsGeneratorOptions options)
        {
            if (
                type.Any(
                    p =>
                        (p.CollectionInnerTypes != null && p.CollectionInnerTypes.Any(q => !q.IsPrimitiveType)) ||
                        p.TransformablePropertyType == PropertyBag.TransformablePropertyTypeEnum.ReferenceType))
            {
                sb.AppendLine(
                    $"{options.OutputNamespace} {Helpers.GetName(type.First().TypeName, options.ClassNameConstantsToRemove)} {{ \n constructor(data) {{ ");
            }
            else if (type.First().TypeDefinition.IsEnum)
            {
                sb.AppendLine(
                    $"{options.OutputNamespace} {Helpers.GetName(type.First().TypeName, options.ClassNameConstantsToRemove)} {{ \n constructor(data) {{ ");
            }
            else
            {
                sb.AppendLine(
                    $"{options.OutputNamespace} {Helpers.GetName(type.First().TypeName, options.ClassNameConstantsToRemove)} {{ \n constructor(data) {{ ");
            }
        }
        private static void BuildPrimitiveProperty(PropertyBag propEntry, StringBuilder sb, JsGeneratorOptions options)
        {
            if (propEntry.TypeDefinition.IsEnum)
            {
                sb.AppendLine(
                    propEntry.PropertyType == typeof(string)
                        ? $"\t{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}: '{propEntry.DefaultValue}',"
                        : $"\t{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}: {propEntry.DefaultValue},");
            }
            else if (propEntry.HasDefaultValue)
            {
                var writtenValue = propEntry.DefaultValue is bool
                    ? propEntry.DefaultValue.ToString().ToLower()
                    : propEntry.DefaultValue;
                sb.AppendLine(
                    $"\tif (!data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}) {{");
                sb.AppendLine(
                    propEntry.PropertyType == typeof(string)
                        ? $"\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = '{writtenValue}';"
                        : $"\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = {writtenValue};");
                sb.AppendLine("\t} else {");
                sb.AppendLine(
                    $"\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)};");
                sb.AppendLine("\t}");
            }
            else
            {
                sb.AppendLine(
                    $"\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)};");
            }
        }

        private static void BuildObjectProperty(StringBuilder sb, PropertyBag propEntry, JsGeneratorOptions options)
        {
            sb.AppendLine(
                $"\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = new {Helpers.GetName(propEntry.PropertyType.Name, options.ClassNameConstantsToRemove)}(data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)});");
        }

        private static void BuildArrayProperty(StringBuilder sb, PropertyBag propEntry, JsGeneratorOptions options)
        {
            var collectionType = propEntry.CollectionInnerTypes.First();
            if (!collectionType.IsPrimitiveType)
            {
                string nameOfMapVar = Helpers.GetName(collectionType.Type.Name, options.ClassNameConstantsToRemove);

                sb.AppendLine(
                    $"\tconst mapped{nameOfMapVar} = data.{Helpers.GetName(collectionType.Type.Name, options.ClassNameConstantsToRemove)}.map(s => new {Helpers.GetName(collectionType.Type.Name, options.ClassNameConstantsToRemove)}(s));");

                sb.AppendLine(
                    $"\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = ko.observableArray(mapped);");
            }
            else
            {
                sb.AppendLine(
                    $"\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = ko.observableArray(data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)});");
            }
        }

        private static void BuildDictionaryProperty(StringBuilder sb, PropertyBag propEntry, JsGeneratorOptions options)
        {
            sb.AppendLine($"\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} = {{}};");
            sb.AppendLine($"\tif(data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)} != null) {{");
            sb.AppendLine(
                $"\t\tfor (var key in data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}) {{");
            sb.AppendLine(
                $"\t\t\tif (data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}.hasOwnProperty(key)) {{");

            var keyType = propEntry.CollectionInnerTypes.First(p => p.IsDictionaryKey);
            if (!AllowedDictionaryKeyTypes.Contains(keyType.Type))
            {
                throw new Exception(
                    $"Dictionaries must have strings, enums, or integers as keys, error found in type: {propEntry.TypeName}");
            }
            var valueType = propEntry.CollectionInnerTypes.First(p => !p.IsDictionaryKey);

            if (!valueType.IsPrimitiveType)
            {
                sb.AppendLine(
                    $"\t\t\t\tif (!overrideObj.{Helpers.GetName(valueType.Type.Name, options.ClassNameConstantsToRemove)}) {{");
                sb.AppendLine(
                    $"\t\t\t\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key] = new {options.OutputNamespace} {Helpers.GetName(valueType.Type.Name, options.ClassNameConstantsToRemove)}(cons.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key]);");
                sb.AppendLine("\t\t\t\t} else {");
                sb.AppendLine(
                    $"\t\t\t\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key] = new overrideObj.{Helpers.GetName(valueType.Type.Name, options.ClassNameConstantsToRemove)}(cons.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key], overrideObj);");

                sb.AppendLine("\t\t\t\t}");
            }
            else
            {
                sb.AppendLine(
                    $"\t\t\t\tthis.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key] = data.{Helpers.ToCamelCase(propEntry.PropertyName, options.CamelCase)}[key];");
            }
            sb.AppendLine("\t\t\t}");
            sb.AppendLine("\t\t}");
            sb.AppendLine("\t}");
        }
    }
}
